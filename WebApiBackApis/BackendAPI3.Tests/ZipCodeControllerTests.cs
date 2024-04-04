using BackendAPI3.Service;
using BackendAPI3.Service.Controllers;
using BackendAPI3.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BackendAPI3.Tests
{
    public class ZipCodeControllerTests
    {
        private ZipCodesService _zipCodesService;
        private ZipCodeController _zipCodeController;
        private readonly Mock<ILogger<ZipCodeController>> _mockLogger;

        public ZipCodeControllerTests()
        {
            _zipCodesService = new ZipCodesService();
            _mockLogger = new Mock<ILogger<ZipCodeController>>();
            _zipCodeController = new ZipCodeController(_zipCodesService, _mockLogger.Object);
        }

        [Fact]
        public void GetAllTest()
        {
            //Act
            var results = _zipCodeController.Get();

            //Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void GetTest()
        {
            //Arrange
            int zip = 22222;
            int wrongZip = 99999;

            //Act
            var result1 = _zipCodeController.Get(zip);
            var result2 = _zipCodeController.Get(wrongZip);

            //Assert
            Assert.NotNull(result1);
            Assert.IsType<OkObjectResult>(result1);
            var okResult = result1 as OkObjectResult;
            var zipCode = okResult.Value as ZipCode;
            Assert.Equal(zip, zipCode.Zip);

            Assert.IsType<NotFoundResult>(result2);

        }

        [Fact]
        public void PostTest()
        {
            //Arrange
            ZipCode zipCode = new() { Zip = 77001, City="Houston", County="Harris", State="TX"};

            //Act, Assert
            var outcome = _zipCodeController.Post(zipCode);
            Assert.IsType<CreatedResult>(outcome);

            outcome = _zipCodeController.Post(zipCode);
            Assert.IsType<BadRequestResult>(outcome);
        }

    }
}
