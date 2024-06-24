using Mc2.CrudTest.DomainModel.DTO.Customer;
using Mc2.CrudTest.Presentation.BusinessServiceContract;
using Mc2.CrudTest.Presentation.Server.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Mc2.CrudTest.Presentation.Client.ViewModel;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBuss _buss;

        public CustomerController(ICustomerBuss buss)
        {
            _buss = buss;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _buss.GetAll();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> AddData([FromBody] CustomerAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.PhoneNumber.StartsWith("0"))
                {
                    model.PhoneNumber = "+98" + model.PhoneNumber.Substring(1);
                }
                
                CustomerAddModel newModel = new CustomerAddModel
                {
                    FirstName = model.FirstName.ToLower(),
                    LastName = model.LastName.ToLower(),
                    BankAccountNumber = model.BankAccountNumber,
                    DateOfBirth = model.DateOfBirth,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                };

                // Assuming _buss.Add is an asynchronous method
                var result = await _buss.Add(newModel);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                string message = "";
                foreach (var messageItem in errorMessages)
                {
                    message += "-" + messageItem;
                }
                return BadRequest(errorMessages);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var result = await _buss.Delete(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody] CustomerUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.PhoneNumber.StartsWith("0"))
                {
                    model.PhoneNumber = "+98" + model.PhoneNumber.Substring(1);
                }

                CustomerUpdateModel newModel = new CustomerUpdateModel
                {
                    FirstName = model.FirstName.ToLower(),
                    LastName = model.LastName.ToLower(),
                    BankAccountNumber = model.BankAccountNumber,
                    DateOfBirth = model.DateOfBirth,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    CustomerId = model.CustomerId
                };

                var result = await _buss.Update(newModel);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                string message = "";
                foreach (var messageItem in errorMessages)
                {
                    message += "-" + messageItem;
                }
                return BadRequest(errorMessages);
            }
        }

    }
}