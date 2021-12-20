using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsApi.Context;
using ReportsApi.DTO;
using ReportsApi.Models;
using ReportsApi.Services;
using ReportsApi.Services.IServices;

namespace ReportsApi.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _employeeService.FindAllEmployees();
        }

        [HttpPost]
        public async Task<Employee> CreateEmployee([FromBody] CreateEmployeeArguments employeeDto)
        {
            var newEmployee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                LeaderId = employeeDto.LeaderId,
                Name = employeeDto.Name,
                PositionName = employeeDto.PositionName,
                Surname = employeeDto.Surname,
                Type = employeeDto.Type
            };
            
            return await _employeeService.Create(newEmployee);
        }

        [HttpPost("add-work-task/{id}")]
        public async Task AddTaskToEmployee([FromBody] TaskDTO taskDto)
        {
            var newTask = new WorkTask()
            {
                TaskState = taskDto.TaskState,
                TaskEditTime = DateTime.Now,
                TaskCreationTime = DateTime.Now,
                Comment = taskDto.Comment
            };
            await _employeeService.AddTask(taskDto.ExecutorId, newTask);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            Employee employee = await _employeeService.FindById(id);
            if (employee == null) return NotFound();
            return employee;
        }

        [HttpGet]
        [Route("info")]
        public async Task<ActionResult<Employee>> GetEmployee
        (
            [FromQuery] string name,
            [FromQuery] string surname
        )
        {
            Employee employee = await _employeeService.FindByNameAndSurname(name, surname);
            if (employee == null) return NotFound();
            return employee;
        }

        [HttpPut("update-employee")]
        public async Task UpdateEmployee([FromBody] EmployeeDTO employeeDto)
        {
            await _employeeService.Update(employeeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _employeeService.Delete(id);
            return NoContent();
        }

        private bool EmployeeExists(Guid id)
        {
            return _employeeService.Exist(id);
        }
    }
}