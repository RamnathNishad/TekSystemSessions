using EFCoreDatabaseFirstLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPI
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmpDataAccess dal;
        public EmployeeController(IEmpDataAccess dal)
        {
            this.dal = dal;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        [Route("GetAllEmps")]
        public IActionResult GetAllEmps()
        {
            try
            {
                return Ok(dal.GetEmps());
            }
            catch (Exception ex)
            {
                return BadRequest("Some database error occurred");
            }
        }

        [HttpGet]
        [Route("GetEmpsByDid/{id}")]
        public IActionResult GetEmpsByDid(int id)
        {
            try
            {
                var result = dal.GetEmps().Where(o => o.Deptid == id).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // GET api/<ValuesController>/5
        [HttpGet]
        [Route("GetEmpById/{id}")]
        //[Route("FindEmpById/{id}")]
        public IActionResult GetEmpById(int id)
        {
            try
            {
                return Ok(dal.GetEmpById(id));
            }
            catch (Exception ex)
            {
                return  BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult AddEmployee([FromBody] Employee emp)
        {
            try
            {
                dal.AddEmployee(emp);
                return Ok("Record inserted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        [Route("UpdateEmpById/{id}")]
        public IActionResult UpdateEmpById(int id, [FromBody] Employee emp)
        {
            try
            {
                dal.UpdateEmp(emp);
                return Ok("Record updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete]
        [Route("DeleteEmpById/{id}")]
        public IActionResult DeleteEmpById(int id)
        {
            try
            {
                dal.DeleteEmpById(id);
                return Ok("Record deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("DivideNumbers/{a}/{b}")]
        public int DivideNumbers(int a, int b)
        {
            return a/b;
        }
     
    }
}
