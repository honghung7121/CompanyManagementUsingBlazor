using BusinessObjects;
using BusinessObjects.Dto;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using WebAPI.Controllers;

namespace Server.UnitTest.Controller
{
    public class CompanyControllerTests
    {
        [Fact]
        public async Task GetCompanies_Returns_OkResult_With_Companies()
        {
            // Arrange
            var fakeRepository = A.Fake<ICompanyRepository>();
            var companies = new List<Company>
            {
                new Company { Id = 1, Name = "Company 1" },
                new Company { Id = 2, Name = "Company 2" }
            };
            A.CallTo(() => fakeRepository.GetCompanies()).Returns(companies);

            var controller = new CompanyController(fakeRepository);

            // Act
            var result = await controller.GetCompanies() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var resultData = result.Value as List<Company>;
            Assert.NotNull(resultData);
            Assert.Equal(2, resultData.Count);
        }
        [Fact]
        public async Task GetCompanies_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            var fakeRepository = A.Fake<ICompanyRepository>();
            A.CallTo(() => fakeRepository.GetCompanies()).Throws(new Exception("Test exception"));

            var controller = new CompanyController(fakeRepository);

            // Act
            var result = await controller.GetCompanies() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);

            var errorMessage = result.Value as string;
            Assert.NotNull(errorMessage);
            Assert.Equal("Test exception", errorMessage);
        }
        [Fact]
        public async Task GetCompany_Returns_OkResult_With_Company()
        {
            // Arrange
            var fakeRepository = A.Fake<ICompanyRepository>();
            var companyId = 1;
            var company = new Company { Id = companyId, Name = "Company 1" };
            A.CallTo(() => fakeRepository.GetCompany(companyId)).Returns(company);

            var controller = new CompanyController(fakeRepository);

            // Act
            var result = await controller.GetCompany(companyId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var resultData = result.Value as Company;
            Assert.NotNull(resultData);
            Assert.Equal(companyId, resultData.Id);
        }
        [Fact]
        public async Task GetCompany_Returns_NotFoundResult_When_Company_Not_Found()
        {
            // Arrange
            var fakeRepository = A.Fake<ICompanyRepository>();
            var companyId = 1;

            // Cấu hình giả lập để trả về null khi GetCompany được gọi với id tương ứng.
            A.CallTo(() => fakeRepository.GetCompany(companyId)).Returns(null as Company);

            var controller = new CompanyController(fakeRepository);

            // Act
            var result = await controller.GetCompany(companyId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetCompany_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            var fakeRepository = A.Fake<ICompanyRepository>();
            var companyId = 1;
            A.CallTo(() => fakeRepository.GetCompany(companyId)).Throws(new Exception("Test exception"));

            var controller = new CompanyController(fakeRepository);

            // Act
            var result = await controller.GetCompany(companyId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);

            var errorMessage = result.Value as string;
            Assert.NotNull(errorMessage);
            Assert.Equal("Test exception", errorMessage);
        }
        [Fact]
        public async Task CreateCompany_Returns_CreatedAtAction_When_Company_Created()
        {
            // Arrange
            var fakeRepository = A.Fake<ICompanyRepository>();
            var companyDto = new CompanyDto { /* Thông tin công ty mới */ };
            var createdCompany = new Company { Id = 1, /* Thông tin công ty đã được tạo */ };

            A.CallTo(() => fakeRepository.CreateCompany(companyDto)).Returns(createdCompany);

            var controller = new CompanyController(fakeRepository);

            // Act
            var result = await controller.CreateCompany(companyDto) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("GetCompany", result.ActionName);
            Assert.Equal(201, result.StatusCode);

            var companyId = (int)result.RouteValues["id"];
            Assert.Equal(createdCompany.Id, companyId);
        }

        [Fact]
        public async Task CreateCompany_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            var fakeRepository = A.Fake<ICompanyRepository>();
            var companyDto = new CompanyDto { /* Thông tin công ty mới */ };

            A.CallTo(() => fakeRepository.CreateCompany(companyDto)).Throws(new Exception("Test exception"));

            var controller = new CompanyController(fakeRepository);

            // Act
            var result = await controller.CreateCompany(companyDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);

            var errorMessage = result.Value as string;
            Assert.NotNull(errorMessage);
            Assert.Equal("Test exception", errorMessage);
        }
    }
}
