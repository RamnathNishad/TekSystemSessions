DQL:- Data Query Language

select * from tbl_employee
select * from tbl_department

insert into tbl_employee values('ravi',1111,201),
								('rahul',2222,202),
								('ramesh',3333,203)




insert into tbl_employee values('rohit',4444,203),
								('suresh',5555,202)

delete  from tbl_employee where ecode=4 or ecode=5



create table test 
(
 ecode int primary key identity (100,1),
 ename varchar(20)
)

insert into test values('ravi'),('rohit')

select * from test

select * from tbl_department

insert into tbl_department values(201,'Account',109),
									(202,'Admin',107),
									(203,'Sales',108)

--filter records having salary greater than 2000 and deptid is 202
select * 
from tbl_employee 
where salary>2000 and deptid=202

--and , or , not

--selecting fewer columns
select ecode,salary from tbl_employee

--select records with bonus as 10% of salary i.e. computed column
select *,0.1*salary as bonus
from tbl_employee

--selecting records having null values
insert into tbl_employee(ename,deptid) values('ramnath',202)
select * from tbl_employee

select * from tbl_employee where salary is null
--Note: NULL in RDBMS means no data, it does not mean to be 0 or empty string ''.

--using constant values with select statement
select 'employee '+convert(varchar(6),ecode)+' works in deptid '+convert(varchar(5),deptid) from tbl_employee


--sorting of records by salary ascending/descending
select * from tbl_employee order by salary desc,deptid desc --thenby sorting

---Nested queries or sub-query : query within another query
--where a query needs data which is not directly available, we need write nested
--query inside the main query to get the required data.
--e.g.
--Q. Find the employees working in deptid in which employee 1005 is working

select * 
from tbl_employee 
where deptid=(select deptid 
			  from tbl_employee 
			  where ecode=1005)

--Q. Find the employees working in deptid in which employee 1005 or employee 1006
--are working
select * 
from tbl_employee 
where deptid IN (select deptid 
				from tbl_employee 
				where ecode=1005 or ecode=1006)

--Note: when the nested query returns multiple values, we cannot use single-valued
--operators like =,!=,<,<=,>,>=.
--instead we shud use multi-valued operators:
-- IN :- compare either of the values , i.e, equal-to any of the values
-- ANY :- >ANY,<ANY
-- ALL :- >ALL,<ALL
    
	12>ALL(5,6,10) ----greater than the maximum
	4<ALL(5,6,10)  ----lesser than the minimum


--Correlated-query:- Nested query but different than normal nested query.
--Normal nested query is executed only once and cached and then main query uses
--it everytime from the cached value without executing nested query again and again.

--But in correlated query, nested query is executed for each record of the main query.

--Q. find the employees whose salary is greater than the average salary of all the
--employees of his department
select *
from tbl_employee as o
where salary>(select avg(i.salary) 
			 from tbl_employee as i
			 where i.deptid=o.deptid
			 )

--Q. Find the students who got marks greater than the average marks of his/her class

select *
from student as o
where marks>(select avg(i.marks)
			 from student as i
			 where i.classid=o.classid)


--grouping of records and group functions
select deptid,sum(salary) as TotalSal, 
max(salary) as MaxSal, 
min(salary) as MinSal, 
avg(isnull(salary,0)) as AvgSal, 
count(isnull(salary,0)) as NoOfEmps
from tbl_employee
group by deptid






select * 
from tbl_employee 
where deptid=(select deptid 
			  from tbl_employee 
			  where ecode=1005)
use empdb

----------------in-built functions--------------------
--a) character functions:-
select left('welcome',3)
select right('welcome',3)
select SUBSTRING('welcome',3,4)
select CONCAT('hello',' world','-welcome',' to',' India')
select REPLACE('india','n','N')
select REPLICATE('*#',100)
select RTRIM('      welcome    ')

--b) mathematical functions:-
select SQRT(16)
select LOG10(10)
select ROUND(12.56734,2)
select FLOOR(-12.267),CEILING(-12.267), ROUND(12.567,0)
--Floor---> while rounding it will make sure that the number is lesser than 
--the original number
--Ceiling:- while rounding it will make sure that the number is greater than 
--the original number

--c) date and time functions
--to get current system date and time
select GETDATE() as Current_Date_Time

--to get the year part of the date
select YEAR(getdate()),MONTH(getdate()),DAY(getdate())
--to get the time parts from the date value
select DATEPART(HH,getdate()),DATEPART(MI,getdate()),DATEPART(SS,getdate())
--to convert into a date formats
select CONVERT(varchar(25),getdate(),100)
select CONVERT(varchar(25),getdate(),101)
select CONVERT(varchar(25),getdate(),102)
select CONVERT(varchar(25),getdate(),103)
select CONVERT(varchar(25),getdate(),114)

--manipulating date 
select DATEADD(YY,2,getdate())

select DATEDIFF(HH,getdate(),'22-JUN-2025')

drop table test
create table test
(
	ecode int primary key,
	doj date
)

insert into test values(101,getdate()),(102,'25-MAY-2025')

select * from test

--Q. find the employee who joined in the month of MAY
select * from test where month(doj)=5

--Q. Find the employees who have joined the company for atleast 5 years
select * from test where year(getdate())-year(doj)>=5



------------JOIN Queries------------------------

--It is used to join records which are present at multiple places in the database
--due to normalization. 
--Types of joins:
--a) INNER JOIN:- only the matching records are joined and unmatched records
--are excluded in the query 

select e.ecode,e.ename,e.salary,d.deptid,d.dname,d.dhead 
from tbl_employee as e
join tbl_department as d
on(e.deptid=d.deptid)

--ansi syntax
select e.ecode,e.ename,e.salary,d.deptid,d.dname,d.dhead 
from tbl_employee as e,tbl_department as d
where e.deptid=d.deptid

--b) Outer Join:-

--i) left outer join:-matched from both and unmatched from left-side table
select e.ecode,e.ename,e.salary,d.deptid,d.dname,d.dhead 
from tbl_employee as e
left outer join tbl_department as d
on(e.deptid=d.deptid)
--ii) right outer join:-matched from both and unmatched from right-side table
select e.ecode,e.ename,e.salary,d.deptid,d.dname,d.dhead 
from tbl_employee as e
right outer join tbl_department as d
on(e.deptid=d.deptid)
--iii) full outer join:-matched from both and unmatched from both-side tables
select e.ecode,e.ename,e.salary,d.deptid,d.dname,d.dhead 
from tbl_employee as e
full outer join tbl_department as d
on(e.deptid=d.deptid)


--self-join:-when table is joined with itself it is self-join
select * 	
from tbl_employee as e	
join tbl_employee as m	
on (e.mgrid=m.ecode)	






select * from tbl_employee
select * from tbl_department

insert into tbl_department values(205,'training',101)




