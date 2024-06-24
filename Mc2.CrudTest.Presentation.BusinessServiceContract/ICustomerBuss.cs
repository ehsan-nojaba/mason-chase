using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.DomainModel.DTO.Customer;
using Mc2.CrudTest.DomainModel.Model;
using Mc2.CrudTest.Presentation.FrameWork;

namespace Mc2.CrudTest.Presentation.BusinessServiceContract
{
    public interface ICustomerBuss
    {
        Task<OperationResult> Delete(int key);
        Task<OperationResult> Add(CustomerAddModel model);
        Task<OperationResult> Update(CustomerUpdateModel model);
        Task<Customer> GetModel(int key);
        List<Customer> GetAll();
    }
}
