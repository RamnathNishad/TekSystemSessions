using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDBCrudDemoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDBCrudDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMongoClient _client;
        public EmployeeController(IMongoClient mongoClient  )
        {
            _client = mongoClient;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            //get the database
            var db = _client.GetDatabase("employeedb");
            //get the collection from the database
            var lstEmps = db.GetCollection<Employee>("employees")
                            .Find(o=>true)
                            .ToList();

            return lstEmps;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            //get the database
            var db = _client.GetDatabase("employeedb");
            //get the collection from the database
            var emp = db.GetCollection<Employee>("employees")
                            .Find(o => o.Ecode == id)
                            .SingleOrDefault();
            return emp;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] Employee emp)
        {
            var db = _client.GetDatabase("employeedb");
            var emps = db.GetCollection<Employee>("employees");
            emps.InsertOne(emp);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut]
        public void Put([FromBody] Employee emp)
        {
            //get the database
            var db = _client.GetDatabase("employeedb");
            //get the collection from the database
            var lstEmps = db.GetCollection<Employee>("employees")
                            .ReplaceOne(e => e.Ecode == emp.Ecode, emp);

        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //get the database
            var db = _client.GetDatabase("employeedb");
            //get the collecion filter by ecode and delete
            var emp = db.GetCollection<Employee>("employees")
                        .DeleteOne(o => o.Ecode == id);                    
        }
    }
}
