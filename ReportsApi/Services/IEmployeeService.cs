using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportsApi.Models;

namespace ReportsApi.Services
{
    public interface IEmployeeService
    {
        Task<Employee> Create(Employee employee);

        Task<Employee> FindByNameAndSurname(string name, string surname);

        Task<Employee> FindById(Guid id);

        Task Delete(Guid id);

        Task<Employee> Update(Guid id, Employee entity);

        Task<List<Employee>> FindAllEmployees();

        Task PutEmployee(Employee employee);

        bool Exist(Guid id);
    }
}