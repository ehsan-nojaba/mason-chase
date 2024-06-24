using Mc2.CrudTest.DomainModel.DTO.Customer;
using Mc2.CrudTest.Presentation.BusinessServiceContract;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    public class Customer2Controller : Controller
    {
        private ICustomerBuss _buss;

        public Customer2Controller(ICustomerBuss buss)
        {
            _buss = buss;
        }
        public IActionResult Index()
        {
            var getAll =  _buss.GetAll();
            List<CustomerListItem> newList = new List<CustomerListItem>();
            foreach (var test in getAll)
            {
                CustomerListItem model = new CustomerListItem
                {
                    CustomerId = test.CustomerId,
                    DateOfBirth = test.DateOfBirth,
                    BankAccountNumber = test.BankAccountNumber,
                    FirstName = test.FirstName,
                    LastName = test.LastName,
                    Email = test.Email,
                    PhoneNumber = test.PhoneNumber,
                };
                newList.Add(model);
            }
            return View(newList);
        }
    }
}
