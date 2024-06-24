using System;
using System.Threading.Tasks;
using Mc2.CrudTest.DomainModel.DTO.Customer;
using Mc2.CrudTest.DomainModel.Model;
using Mc2.CrudTest.Presentation.DataAccessServiceContract;
using Mc2.CrudTest.Presentation.FrameWork;

public interface ICustomerRepository:IBaseRepository<int, Customer , CustomerAddModel , CustomerUpdateModel>
{
    Task<bool> HasFirstNameAlreadyExist(string firstName);
    Task<bool> HasFirstNameAlreadyExist(string firstName, int customerId);
    Task<bool> HasLastNameAlreadyExist(string lastName);
    Task<bool> HasLastNameAlreadyExist(string lastName, int customerId);
    Task<bool> HasEmailAlreadyExist(string email);
    Task<bool> HasEmailAlreadyExist(string email, int customerId);
    Task<bool> HasDateOfBirthAlreadyExist(DateTime dateOfBirth);
    Task<bool> HasDateOfBirthAlreadyExist(DateTime dateOfBirth, int customerId);
}