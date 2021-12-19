using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportsApi.Context;
using ReportsApi.DTO;
using ReportsApi.Models;
using ReportsApi.Services.IServices;

namespace ReportsApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ReportsDbContext _context;

        public EmployeeService(ReportsDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> Create(Employee employee)   
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task<List<Employee>> FindAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> FindByNameAndSurname(string name, string surname)
        {
            Employee employee =
                await _context.Employees
                    .FirstOrDefaultAsync(x => x.Name == name && x.Surname == surname);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> FindById(Guid id)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task PutEmployee(Employee employee)
        {
            Employee currentEmployee = await _context.Employees.FindAsync(employee);
            _context.Entry(currentEmployee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task AddTask(Guid id, WorkTask task)
        {
            Employee currentEmployee = await _context.Employees.FindAsync(id);
            task.Executor = currentEmployee;
            task.Executor.EmployeeId = currentEmployee.EmployeeId;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> Update(EmployeeDTO entity)
        {
            Employee employee = await _context.Employees.FindAsync(entity.Id);
            if (employee is null) throw new ArgumentException(nameof(employee) + "is invalid");
            employee.Name = entity.Name;
            employee.Surname = entity.Surname;
            employee.LeaderId = entity.LeaderId;
            employee.PositionName = entity.PositionName;
            employee.Type = entity.Type;
            await _context.SaveChangesAsync();
            return employee;
        }

        public bool Exist(Guid id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}