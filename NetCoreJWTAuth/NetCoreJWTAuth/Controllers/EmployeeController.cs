using ApplicationLayer.Interface;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreJWTAuth.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("OIPAPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IEmployeeAppService _employeeService;
        public EmployeeController(IEmployeeAppService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("getEmployeeList")]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeService.GetEmployeeList();
            return Ok(result);
        }

        [HttpGet("getEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await _employeeService.GetEmployeeById(x => x.EmpId == id);
            return Ok(result);
        }

        [HttpPost("saveEmployee")]
        public async Task<IActionResult> SaveEmployee([FromBody] Employee employee)
        {
            var result = await _employeeService.AddEmployee(employee);
            return Ok(result);
        }

        [HttpPut("updateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            var result = await _employeeService.UpdateEmployee(employee);
            return Ok(result);
        }

        [HttpDelete("deleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            return Ok(result);
        }
    }
}
