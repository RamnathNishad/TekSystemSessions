using EFCoreDatabaseFirstLib;
using Microsoft.AspNetCore.Mvc;
using MVCHelloWorld.Models;

namespace MVCHelloWorld.Controllers
{
    public class EmployeeController : Controller
    {
        //static List<Employee> lstEmps=new List<Employee>();

        private readonly IEmpDataAccess dal;
        public EmployeeController(IEmpDataAccess dal)
        {
            this.dal= dal;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Home()
        {
            var emp = dal.GetEmpById(101);
            return View(emp);
        }
        public IActionResult DisplayEmps()
        {
            var lstEmps = dal.GetEmps();
            return View(lstEmps);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            dal.AddEmployee(emp);
            return RedirectToAction("DisplayEmps");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            dal.DeleteEmpById(id);
            return RedirectToAction("DisplayEmps");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //find the record by id
            var emp = dal.GetEmpById(id);
            //display the record in the view for editing
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            //find the record to update
            dal.UpdateEmp(emp);
            return RedirectToAction("DisplayEmps");
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            //find the record to display
            var emp = dal.GetEmpById(id);

            return View(emp);
        }
        public IActionResult GetData()
        {
            var msg = "Welcome";
            var x = 100;

            var y = 200;
            ViewBag.y = y;

            ViewData.Add("message", msg);
            TempData.Add("b", x);

            return View();
        }

        public IActionResult SecondPage()
        {
            var a = TempData["b"];
            TempData.Keep();
            ViewData.Add("a", a);
            return View();
        }



    }
}
