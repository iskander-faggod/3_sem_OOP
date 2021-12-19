using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportsApi.DTO;
using ReportsApi.Models;

namespace ReportsApi.Services.IServices
{
    public interface IEmployeeService
    {
        Task<Employee> Create(Employee employee);

        Task<Employee> FindByNameAndSurname(string name, string surname);

        Task<Employee> FindById(Guid id);

        Task Delete(Guid id);

        Task<Employee> Update(EmployeeDTO entity);

        Task<List<Employee>> FindAllEmployees();

        Task PutEmployee(Employee employee);

        Task AddTask(Guid id, WorkTask task);

        bool Exist(Guid id);
    }
}