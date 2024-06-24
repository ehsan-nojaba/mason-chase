using System.Threading.Tasks;
using Moq;
using Xunit;
using FluentAssertions;
using Mc2.CrudTest.Presentation.Business;
using Mc2.CrudTest.Presentation.BusinessServiceContract;
using Mc2.CrudTest.DomainModel.DTO.Customer;
using Mc2.CrudTest.DomainModel.Model;
using PhoneNumbers;
using Mc2.CrudTest.Presentation.FrameWork;

namespace Mc2.CrudTest.Tests
{
    public class CustomerBussTests
    {
        private readonly Mock<ICustomerRepository> _mockRepository;
        private readonly CustomerBuss _customerBuss;

        public CustomerBussTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _customerBuss = new CustomerBuss(_mockRepository.Object);
        }

        [Fact]
        public async Task Delete_Should_Return_Success()
        {
            // Arrange
            int customerId = 1;
            var operationResult = new OperationResult("Delete", "Customer").ToSuccess("Delete Successfully");
            _mockRepository.Setup(repo => repo.Delete(customerId)).ReturnsAsync(operationResult);

            // Act
            var result = await _customerBuss.Delete(customerId);

            // Assert
            result.Should().BeEquivalentTo(operationResult);
        }

        [Fact]
        public async Task Add_Should_Return_Fail_If_FirstName_Exists()
        {
            // Arrange
            var model = new CustomerAddModel { FirstName = "John", LastName = "Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 1, 1), PhoneNumber = "+989123456789" };
            _mockRepository.Setup(repo => repo.HasFirstNameAlreadyExist(model.FirstName)).ReturnsAsync(true);

            // Act
            var result = await _customerBuss.Add(model);

            // Assert
            result.Should().BeEquivalentTo(new OperationResult("Add", "Customer").ToFail("This FirstName Has Already Exist"));
        }

        [Fact]
        public async Task Add_Should_Return_Success()
        {
            // Arrange
            var model = new CustomerAddModel { FirstName = "John", LastName = "Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 1, 1), PhoneNumber = "+989123456789" };
            _mockRepository.Setup(repo => repo.HasFirstNameAlreadyExist(model.FirstName)).ReturnsAsync(false);
            _mockRepository.Setup(repo => repo.HasLastNameAlreadyExist(model.LastName)).ReturnsAsync(false);
            _mockRepository.Setup(repo => repo.HasEmailAlreadyExist(model.Email)).ReturnsAsync(false);
            _mockRepository.Setup(repo => repo.HasDateOfBirthAlreadyExist(model.DateOfBirth)).ReturnsAsync(false);
            _mockRepository.Setup(repo => repo.Add(model)).ReturnsAsync(new OperationResult("Add", "Customer").ToSuccess("add successfully"));

            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse(model.PhoneNumber, "IR");

            // Act
            var result = await _customerBuss.Add(model);

            // Assert
            result.Should().BeEquivalentTo(new OperationResult("Add", "Customer").ToSuccess("Add Successfully"));
        }

        [Fact]
        public async Task Update_Should_Return_Fail_If_FirstName_Exists()
        {
            // Arrange
            var model = new CustomerUpdateModel { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 1, 1), PhoneNumber = "+989123456789" };
            _mockRepository.Setup(repo => repo.HasFirstNameAlreadyExist(model.FirstName, model.CustomerId)).ReturnsAsync(true);

            // Act
            var result = await _customerBuss.Update(model);

            // Assert
            result.Should().BeEquivalentTo(new OperationResult("Update", "Customer").ToFail("This FirstName Has Already Exist"));
        }

        [Fact]
        public async Task Update_Should_Return_Success()
        {
            // Arrange
            var model = new CustomerUpdateModel { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 1, 1), PhoneNumber = "+989123456789" };
            _mockRepository.Setup(repo => repo.HasFirstNameAlreadyExist(model.FirstName, model.CustomerId)).ReturnsAsync(false);
            _mockRepository.Setup(repo => repo.HasLastNameAlreadyExist(model.LastName, model.CustomerId)).ReturnsAsync(false);
            _mockRepository.Setup(repo => repo.HasEmailAlreadyExist(model.Email, model.CustomerId)).ReturnsAsync(false);
            _mockRepository.Setup(repo => repo.HasDateOfBirthAlreadyExist(model.DateOfBirth, model.CustomerId)).ReturnsAsync(false);
            _mockRepository.Setup(repo => repo.Update(model)).ReturnsAsync(new OperationResult("Update", "Customer").ToSuccess("Update Successfully"));

            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse(model.PhoneNumber, "IR");

            // Act
            var result = await _customerBuss.Update(model);

            // Assert
            result.Should().BeEquivalentTo(new OperationResult("Update", "Customer").ToSuccess("Update Successfully"));
        }

        [Fact]
        public async Task GetModel_Should_Return_Customer()
        {
            // Arrange
            int customerId = 1;
            var customer = new Customer { CustomerId = customerId, FirstName = "John", LastName = "Doe" };
            _mockRepository.Setup(repo => repo.GetModel(customerId)).ReturnsAsync(customer);

            // Act
            var result = await _customerBuss.GetModel(customerId);

            // Assert
            result.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public void GetAll_Should_Return_All_Customers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe" },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Doe" }
            };
            _mockRepository.Setup(repo => repo.GetAll()).Returns(customers);

            // Act
            var result = _customerBuss.GetAll();

            // Assert
            result.Should().BeEquivalentTo(customers);
        }
    }
}
