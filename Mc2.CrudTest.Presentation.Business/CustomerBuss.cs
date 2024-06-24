using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.DomainModel.DTO.Customer;
using Mc2.CrudTest.DomainModel.Model;
using Mc2.CrudTest.Presentation.BusinessServiceContract;
using Mc2.CrudTest.Presentation.FrameWork;
using PhoneNumbers;

namespace Mc2.CrudTest.Presentation.Business
{
    public class CustomerBuss:ICustomerBuss
    {
        private ICustomerRepository repo;

        public CustomerBuss(ICustomerRepository repo)
        {
            this.repo = repo;
        }

        public async Task<OperationResult> Delete(int key)
        {
            return await repo.Delete(key);
        }

        public async Task<OperationResult> Add(CustomerAddModel model)
        {
            if (await repo.HasFirstNameAlreadyExist(model.FirstName))
            {
                return new OperationResult("Add", "Customer").ToFail("This FirstName Has Already Exist");
            }

            if (await repo.HasLastNameAlreadyExist(model.LastName))
            {
                return new OperationResult("Add", "Customer").ToFail("This LastName Has Already Exist");
            }

            if (await repo.HasEmailAlreadyExist(model.Email))
            {
                return new OperationResult("Add", "Customer").ToFail("This Email Has Already Exist");
            }

            if (await repo.HasDateOfBirthAlreadyExist(model.DateOfBirth))
            {
                return new OperationResult("Add", "Customer").ToFail("This Date Has Already Exist");
            }

            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse(model.PhoneNumber, "IR");
            if (!(phoneNumberUtil.IsValidNumberForRegion(phoneNumber, "IR") && phoneNumberUtil.GetNumberType(phoneNumber) == PhoneNumberType.MOBILE))
            {
                return new OperationResult("Add", "Customer").ToFail("This Mobile Number is not valid");
            }

            return await repo.Add(model);
        }

        public async Task<OperationResult> Update(CustomerUpdateModel model)
        {
            if (await repo.HasFirstNameAlreadyExist(model.FirstName , model.CustomerId))
            {
                return new OperationResult("Update", "Customer").ToFail("This FirstName Has Already Exist");
            }

            if (await repo.HasLastNameAlreadyExist(model.LastName, model.CustomerId))
            {
                return new OperationResult("Update", "Customer").ToFail("This LastName Has Already Exist");
            }

            if (await repo.HasEmailAlreadyExist(model.Email, model.CustomerId))
            {
                return new OperationResult("Update", "Customer").ToFail("This Email Has Already Exist");
            }

            if (await repo.HasDateOfBirthAlreadyExist(model.DateOfBirth, model.CustomerId))
            {
                return new OperationResult("Update", "Customer").ToFail("This Date Has Already Exist");
            }

            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse(model.PhoneNumber, "IR");
            if (!(phoneNumberUtil.IsValidNumberForRegion(phoneNumber, "IR") && phoneNumberUtil.GetNumberType(phoneNumber) == PhoneNumberType.MOBILE))
            {
                return new OperationResult("Update", "Customer").ToFail("This Mobile Number is not valid");
            }

            return await repo.Update(model);
        }

        public async Task<Customer> GetModel(int key)
        {
            return await repo.GetModel(key);
        }

        public List<Customer> GetAll()
        {
            return repo.GetAll();
        }
    }
}
