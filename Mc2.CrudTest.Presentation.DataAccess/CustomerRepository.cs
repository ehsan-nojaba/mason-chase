using Mc2.CrudTest.DomainModel.DTO.Customer;
using Mc2.CrudTest.DomainModel.Model;
using Mc2.CrudTest.Presentation.DataAccessServiceContract;
using Mc2.CrudTest.Presentation.FrameWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CustomerRepository : ICustomerRepository
{
    private readonly ProjectTestContext db;

    public CustomerRepository(ProjectTestContext db)
    {
        this.db = db;
    }

    public async Task<OperationResult> Delete(int key)
    {
        OperationResult op = new OperationResult("Delete", "Customer");
        try
        {
            var getItem = await GetModel(key);
            if (getItem != null)
            {
                db.Customers.Remove(getItem);
                await db.SaveChangesAsync();
                op.ToSuccess("Delete Successfully");
            }
            else
            {
                op.ToFail("Customer not found");
            }
        }
        catch (Exception e)
        {
            op.ToFail("Delete Fail: " + e.Message);
        }
        return op;
    }

    public async Task<OperationResult> Add(CustomerAddModel model)
    {
        OperationResult op = new OperationResult("Add", "Customer");
        try
        {
            Customer customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                BankAccountNumber = model.BankAccountNumber,
                Email = model.Email,
            };
            await db.Customers.AddAsync(customer);
            await db.SaveChangesAsync();
            op.ToSuccess("Add Successfully");
        }
        catch (Exception e)
        {
            op.ToFail("Add Fail: " + e.Message);
        }
        return op;
    }

    public async Task<OperationResult> Update(CustomerUpdateModel model)
    {
        OperationResult op = new OperationResult("Update", "Customer");
        try
        {
            var getItem = await GetModel(model.CustomerId);
            if (getItem != null)
            {
                getItem.CustomerId = model.CustomerId;
                getItem.FirstName = model.FirstName;
                getItem.LastName = model.LastName;
                getItem.PhoneNumber = model.PhoneNumber;
                getItem.DateOfBirth = model.DateOfBirth;
                getItem.BankAccountNumber = model.BankAccountNumber;
                getItem.Email = model.Email;
                await db.SaveChangesAsync();
                op.ToSuccess("Update Successfully");
            }
            else
            {
                op.ToFail("Customer not found");
            }
        }
        catch (Exception e)
        {
            op.ToFail("Update Fail: " + e.Message);
        }
        return op;
    }

    public async Task<Customer> GetModel(int key)
    {
        return await db.Customers.SingleOrDefaultAsync(x => x.CustomerId == key);
    }

    public List<Customer> GetAll()
    {
        return  db.Customers.OrderBy(x => x.FirstName).ToList();
    }

    public async Task<bool> HasFirstNameAlreadyExist(string firstName)
    {
        return await db.Customers.AnyAsync(x => x.FirstName == firstName);
    }

    public async Task<bool> HasFirstNameAlreadyExist(string firstName, int customerId)
    {
        return await db.Customers.AnyAsync(x => x.FirstName == firstName && x.CustomerId != customerId);
    }

    public async Task<bool> HasLastNameAlreadyExist(string lastName)
    {
        return await db.Customers.AnyAsync(x => x.LastName == lastName);
    }

    public async Task<bool> HasLastNameAlreadyExist(string lastName, int customerId)
    {
        return await db.Customers.AnyAsync(x => x.LastName == lastName && x.CustomerId != customerId);
    }

    public async Task<bool> HasEmailAlreadyExist(string email)
    {
        return await db.Customers.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> HasEmailAlreadyExist(string email, int customerId)
    {
        return await db.Customers.AnyAsync(x => x.Email == email && x.CustomerId != customerId);
    }

    public async Task<bool> HasDateOfBirthAlreadyExist(DateTime dateOfBirth)
    {
        return await db.Customers.AnyAsync(x => x.DateOfBirth == dateOfBirth);
    }

    public async Task<bool> HasDateOfBirthAlreadyExist(DateTime dateOfBirth, int customerId)
    {
        return await db.Customers.AnyAsync(x => x.DateOfBirth == dateOfBirth && x.CustomerId != customerId);
    }
}
