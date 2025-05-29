using EFCoreDatabaseFirstLib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmpDataAccess dal;
        public EmployeeController(IEmpDataAccess dal)
        {
            this.dal = dal;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return dal.GetEmps();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return dal.GetEmpById(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Employee emp)
        {
            dal.AddEmployee(emp);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee emp)
        {
            dal.UpdateEmp(emp);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dal.DeleteEmpById(id);
        }
    }
}
