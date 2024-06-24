using FluentAssertions;
using Mc2.CrudTest.DomainModel.DTO.Customer;
using Mc2.CrudTest.DomainModel.Model;
using Mc2.CrudTest.Presentation.Business;
using Mc2.CrudTest.Presentation.BusinessServiceContract;
using Mc2.CrudTest.Presentation.FrameWork;
using Moq;
using TechTalk.SpecFlow.Assist;

[Binding]
public class CustomerManagerSteps
{
    private readonly ICustomerBuss _customerBuss;
    private OperationResult _result;
    private Customer _customer;
    private readonly Mock<ICustomerRepository> _mockRepository;

    public CustomerManagerSteps()
    {
        _mockRepository = new Mock<ICustomerRepository>();
        _customerBuss = new CustomerBuss(_mockRepository.Object);
    }

    [Given(@"a customer with the following details:")]
    public async Task GivenACustomerWithTheFollowingDetails(Table table)
    {
        var customerDetails = table.CreateInstance<CustomerAddModel>();

        // Mock repository to simulate Add operation
        _mockRepository.Setup(repo => repo.HasFirstNameAlreadyExist(customerDetails.FirstName)).ReturnsAsync(false);
        _mockRepository.Setup(repo => repo.HasLastNameAlreadyExist(customerDetails.LastName)).ReturnsAsync(false);
        _mockRepository.Setup(repo => repo.HasEmailAlreadyExist(customerDetails.Email)).ReturnsAsync(false);
        _mockRepository.Setup(repo => repo.HasDateOfBirthAlreadyExist(customerDetails.DateOfBirth)).ReturnsAsync(false);
        _mockRepository.Setup(repo => repo.Add(It.IsAny<CustomerAddModel>())).ReturnsAsync(new OperationResult("Add", "Customer"));

        _result = await _customerBuss.Add(customerDetails);

        _customer = new Customer
        {
            CustomerId = 1,  // Assuming the ID is generated and returned by the repository
            FirstName = customerDetails.FirstName,
            LastName = customerDetails.LastName,
            Email = customerDetails.Email,
            DateOfBirth = customerDetails.DateOfBirth,
            PhoneNumber = customerDetails.PhoneNumber
        };

        _mockRepository.Setup(repo => repo.GetModel(It.IsAny<int>())).ReturnsAsync(_customer);
        _mockRepository.Setup(repo => repo.Update(It.IsAny<CustomerUpdateModel>())).ReturnsAsync(new OperationResult("Update", "Customer"));
        _mockRepository.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(new OperationResult("Delete", "Customer"));
    }

    [When(@"the operator creates the customer")]
    public void WhenTheOperatorCreatesTheCustomer()
    {
        // This step might already be covered in the Given step
    }

    [Then(@"the customer should be created successfully")]
    public void ThenTheCustomerShouldBeCreatedSuccessfully()
    {
        _result.Should().NotBeNull();
        _result.Success.Should().BeTrue();
    }

    [When(@"the operator updates the customer with new details:")]
    public async Task WhenTheOperatorUpdatesTheCustomerWithNewDetails(Table table)
    {
        var customerDetails = table.CreateInstance<CustomerUpdateModel>();
        customerDetails.CustomerId = _customer.CustomerId;  // Ensure CustomerId is set
        _result = await _customerBuss.Update(customerDetails);
    }

    [Then(@"the customer should be updated successfully")]
    public void ThenTheCustomerShouldBeUpdatedSuccessfully()
    {
        _result.Should().NotBeNull();
        _result.Success.Should().BeTrue();
    }

    [When(@"the operator deletes the customer")]
    public async Task WhenTheOperatorDeletesTheCustomer()
    {
        _result = await _customerBuss.Delete(_customer.CustomerId);
    }

    [Then(@"the customer should be deleted successfully")]
    public void ThenTheCustomerShouldBeDeletedSuccessfully()
    {
        _result.Should().NotBeNull();
        _result.Success.Should().BeTrue();
    }
}
