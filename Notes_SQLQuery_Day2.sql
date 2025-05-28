-------------------T-SQL(Transact-SQL)------------------
--tables,indexes,views,procedures,functions,triggers
--It is a block of SQL statements along with some programming construct to perform
--some task in database.
--for e.g.
	update tbl_employee set salary=salary + 5000
--Q. update the employees salary based on department id as per following assumptions
--	 if employee salary is above 50000 then no increment
--	 if employees deptid is 201 and salary is below 50000 then increment the salary

--how to declare variables in T-SQL block:
--a) pre-defined environment varaibles:- They are reffered with @@env_var
--b) user-defined variables:- They are declared and used with @x or @y
go
create or alter procedure sp_add_numbers(@n1 int=0,@n2 int=0)
as
--declare variables
declare @result int
--calculate result
set @result=@n1 + @n2
--display the result
select @n1 as Num1,@n2 as Num2, @result as [Sum]



--calling stored procedure
exec sp_add_numbers 10,50


--working with IF-ELSE statements
--Q. update the salary of an employee id=101 by 10% only if salary is <50000
--else increment by 5%

go

create or alter procedure sp_updatesal(@ec int)
as
declare @sal int
--get the salary of the emp by id
select @sal=salary from tbl_employee where ecode=@ec
--check if the salary <50000
if @sal<50000
begin
 update tbl_employee set salary=salary+0.1*salary where ecode=@ec
 print 'salary updated for ecode ' + convert(varchar(5),@ec) + ' by 10%'
end
else
begin
  update tbl_employee set salary=salary+0.05*salary where ecode=@ec
  print 'salary updated for ecode ' + convert(varchar(5),@ec) + ' by 5%'
end

 


--execute the stored procedure
exec sp_updatesal 1005

---STORED PROCEDURE--------
--it is a T-SQL block of statement which is compiled and stored in database with 
--some name and that can be called and executed by the applications.
--*it does not return any value, just performs the task written in it.
--*it can be parameterized


---direction of parameters in stored procedure----------
--1) INPUT:- which is the default direction, no need to write it. It means it is
--passed by-value. Any modification inside the block won't be retained after 
--coming out of the block
--2) OUTPUT:- which is like passing the parameter values by reference that means
--address is passed so it retains the modified value after exiting the block


go
create or alter procedure sp_getbonus(@salary int,@bonus decimal out) with encryption
as
if @salary>5000
begin
	set @bonus=0.1*@salary
	print 'bonus-inside the block:'+convert(varchar(10),@bonus) 
end


--call the procedure in another block
declare @salary int=15000,@bonus decimal=0
exec sp_getbonus @salary,@bonus out
print 'bonus-outside the block:'+convert(varchar(10),@bonus) 



--to remove stored procedure
drop procedure sp_add_numbers


--to view the script of the stored procedure
sp_helptext sp_getbonus

sp_help sp_getbonus



---------------user-defined functions--------------------
--These are also same as procedure but it returns a value using RETURN statement
--based on the return type, functions are categorised:
--a) Scalar-valued functions: which returns primitive or scalar value like int,char,
--date,decimal
--b) Table-valued functions:- it returns TABLE and only select query statement result

go

create or alter function get_emp_bonus(@ec int) returns decimal
as
begin
	declare @bonus decimal,@sal int
	select @sal=salary from tbl_employee where ecode=@ec
	set @bonus=@sal*0.1

	return @bonus
end



--calling the function
declare @ec int=1004,@bonus decimal
set @bonus=dbo.get_emp_bonus(@ec)
select @ec as Ecode,@bonus as Bonus




select *,dbo.get_emp_bonus(ecode) as Bonus from tbl_employee


--Note: stored procedure cannot be used as a return value like in select query 
--whereas functions can be used as it returns value.



--Table-valued functions

create or alter function get_emps_by_did(@did int) returns table
as
	return
		(
		select * 
		from tbl_employee
		where deptid=@did
		)


--calling table-valued function
--in simple statement
select * 
from dbo.get_emps_by_did(202)

--in complex queries like joins where records are needed
select * 
from dbo.get_emps_by_did(202) as e,tbl_department as d
where e.deptid=d.deptid



---CASE statements------------
--a) condition-based syntax=>

select *, case 
				when deptid=201 then 0.1*salary
				when deptid=202 then 0.2*salary
				else 0
		  end "Bonus"
from tbl_employee

--b) expression-based syntax=>
select *, case deptid
				when 201 then 0.1*salary
				when 202 then 0.2*salary
				else 0
				end "Bonus"
from tbl_employee

create or alter function fn_getbonus(@did int,@salary int) returns decimal
as
begin
declare @bonus decimal
select @bonus=case @did
				when 201 then 0.1*@salary
				when 202 then 0.2*@salary
				else 0
			  end
return @bonus
end


--calling the above function
select *,dbo.fn_getbonus(deptid,salary) as Bonus from tbl_employee



--looping statement--
declare @n int,@p int,@i int=1
set @n=2
while @i<=10
begin
	set @p=@n*@i
	print @p
	set @i=@i+1
end



create table db_errors
(
	errorid int identity(1,1),
	errnum int,
	errstate int,
	errline int,
	errmsg varchar(max),
	errseverity int,
	errdate datetime
)

go
-----handling error in TSQL block---------
create or alter procedure sp_insert(@ec int)
as
begin try
	---statements---
	insert into test values(@ec,getdate())
	print 'Record inserted'
end try
begin catch
	insert into db_errors values(
			ERROR_NUMBER(),
			ERROR_STATE(),
			ERROR_LINE(),
			ERROR_MESSAGE(),
			ERROR_SEVERITY(),
			getdate()
			)
	print 'error occured, see the errolog table'
end catch






exec sp_insert 103


select [message_id],[severity],[text] from [sys].[messages] where message_id=2627


--------------------Triggers--------------------
--These are TSQL block which gets fired (executed) automatically when operation
--is performed on which it is defined.
--for e.g.
	--triggers can be defined to rollback some unwanted tasks like unwanted record
	--deletions or updations


--a) DML triggers:- triggers are defined on INSERT,DELETE,UPDATE statemenrs. So 
--whenever these statements are executed, trigger block gets fired.

	create or alter trigger del_trig
	on test
	for delete,update
	as
		rollback 
		print 'you cannot delete or update record, it is rolled back'



---there two magical or virtual tables exists in trigger context-------
--1) deleted:- here we can access the deleted values of the record
--2) inserted:- here we can get the new record inserted 
--Note: during update we can access old values from 'deleted' table and new values
--from the 'inserted'

create table log_emp_table
(
	ecode int,
	ename varchar(25),
	salary int,
	deptid int,
	dot datetime
)

create trigger trig2_del
on tbl_employee
for delete
as
	declare @ec int,@en varchar(25),@sal int,@did int
	select @ec=ecode,@en=ename,@sal=salary,@did=deptid from deleted
	--insert this deleted record into log table
	insert into log_emp_table values(@ec,@en,@sal,@did,getdate())
	print 'record deleted and logged inti log table'


delete from tbl_employee where ecode=1004

select * from tbl_employee
select * from log_emp_table

--b) DDL triggers:- CREATE TABLE,DROP TABLE,CREATE PROCEDURE, ALTER TABLE etc

create trigger t1 on database
for drop_table
as
	rollback
	print 'u cannot drop table in this database, contact admin'


drop table tbl_employee
select * from test


drop trigger [trig2_del]

drop trigger t1 on database