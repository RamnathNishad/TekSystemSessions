using EFCodeCodeFirstLib;
using Microsoft.AspNetCore.Mvc;

namespace MVCHelloWorld.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerDataAccess dal;
        public CustomerController(ICustomerDataAccess dal)
        {
            this.dal = dal;
        }
        public IActionResult Index()
        {
            var lstCust = dal.GetCustomers();
            return View(lstCust);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            dal.AddCustomer(customer);
            return RedirectToAction("Index");
        }
    }
}
