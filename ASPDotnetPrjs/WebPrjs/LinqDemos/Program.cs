namespace LinqDemos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lstEmps = Employee.Employees;
            var lstDepts = Department.Departments;

            //1. Find the employees with salary greater than 2000
            var res1=from e in lstEmps
                    where e.Salary>2000
                    orderby e.Salary descending
                    select e;
            //using extension method
            res1 = lstEmps.Where(o => o.Salary > 2000)
                          .OrderByDescending(o=>o.Salary);
            foreach (Employee e in res1)
            {
                //Console.WriteLine($"{e.Ecode}\t{e.Ename}\t{e.Salary}\t{e.Deptid}");
            }

            //2. select only few fields
            var res2 = (from e in lstEmps
                       select new 
                       { 
                           e.Ecode, 
                           e.Ename 
                       }).ToList();
            
            //using lambda
            res2 = lstEmps.Select(o => new { o.Ecode, o.Ename })
                          .ToList();

            foreach (var e in res2)
            {
                Console.WriteLine($"{e.Ecode}\t{e.Ename}");
            }

            //3. Computed values in the query
            var res3 = from e in lstEmps
                       select new
                       {
                           e.Ecode,
                           e.Ename,
                           e.Salary,
                           e.Deptid,
                           Bonus = 0.1*e.Salary
                       };
            foreach (var e in res3)
            {
                Console.WriteLine($"{e.Ecode}\t{e.Ename}\t{e.Salary}\t{e.Deptid}\t{e.Bonus}");
            }
            //4. Group functions
            var grpRes = new
            {
                TotalSal=lstEmps.Sum(o=>o.Salary),
                AvgSalary=lstEmps.Average(o=>o.Salary),
                MaxSalary=lstEmps.Max(o=>o.Salary),
                MinSalary=lstEmps.Min(o=>o.Salary),
                NoOfEmps=lstEmps.Count
            };
            Console.WriteLine($"Total Salary:{grpRes.TotalSal}");
            Console.WriteLine($"Average Salary:{grpRes.AvgSalary}");
            Console.WriteLine($"Max Salary:{grpRes.MaxSalary}");
            Console.WriteLine($"Min Salary:{grpRes.MinSalary}");
            Console.WriteLine($"No. of Emps:{grpRes.NoOfEmps}");

            //5. Group by
            var res4 = from e in lstEmps
                       group e by e.Deptid into g
                       select new
                       {
                           Deptid=g.Key,
                           TotalSal = g.Sum(o => o.Salary),
                           AvgSalary = g.Average(o => o.Salary),
                           MaxSalary = g.Max(o => o.Salary),
                           MinSalary = g.Min(o => o.Salary),
                           NoOfEmps = g.Count()
                       };
            //using extension method
            res4 = lstEmps.GroupBy(o => o.Deptid)
                        .Select(g => new
                        {
                            Deptid = g.Key,
                            TotalSal = g.Sum(o => o.Salary),
                            AvgSalary = g.Average(o => o.Salary),
                            MaxSalary = g.Max(o => o.Salary),
                            MinSalary = g.Min(o => o.Salary),
                            NoOfEmps = g.Count()
                        });

            foreach (var r in res4)
            {
                Console.WriteLine($"{r.Deptid}\t{r.TotalSal}\t{r.AvgSalary}\t{r.MaxSalary}\t{r.MinSalary}\t{r.NoOfEmps}");
            }

            //6. Find the employees working in the depatid id of 
            //employee whose ecode is 101
            var res5 = from e in lstEmps
                       where e.Deptid==(from p in lstEmps
                                        where p.Ecode==101
                                        select p.Deptid).SingleOrDefault()
                       select e;

            foreach (var e in res5)
            {
                //Console.WriteLine($"{e.Ecode}\t{e.Ename}\t{e.Salary}\t{e.Deptid}");
            }


            //7. Joins
            var res7 = from e in lstEmps
                       join d in lstDepts on e.Deptid equals d.Deptid
                       select new
                       {
                           e.Ecode,
                           e.Ename,
                           e.Salary,
                           d.Deptid,
                           d.Dname,
                           d.Dhead
                       };

            //using lambda and extension method
            res7 = lstEmps.Join(lstDepts,
                                e=>e.Deptid,
                                d=>d.Deptid,
                                (e,d)=> new
                                {
                                    e.Ecode,
                                    e.Ename,
                                    e.Salary,
                                    d.Deptid,
                                    d.Dname,
                                    d.Dhead
                                });

            foreach (var e in res7)
            {
                Console.WriteLine($"{e.Ecode}\t{e.Ename}\t{e.Salary}\t{e.Deptid}\t{e.Dname}\t{e.Dhead}");
            }

            //var lstNumbers = new List<int> { 2, 10, 3, 6, 7, 20, 60 };

            //find the numbers which are even numbers
            //var result=new List<int>();
            //foreach (int n in lstNumbers)
            //{
            //    if(n % 2==0)
            //    {
            //        result.Add(n);
            //    }
            //}
            //using LINQ 
            //using operators
            //var result=from n in lstNumbers
            //           where n % 2 ==0
            //           orderby n descending
            //           select n;
            //using extension methods
            //var result=lstNumbers.Where(n=>n % 2 ==0)
            //                     .OrderByDescending(n=>n);

            ////display the result
            //foreach(var n in result)
            //{
            //    Console.WriteLine(n);
            //}

        }
    }
}
