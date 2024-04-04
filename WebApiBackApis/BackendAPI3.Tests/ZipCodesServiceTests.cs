using BackendAPI3.Service;
using BackendAPI3.Service.Models;

namespace BackendAPI3.Tests
{
    public class ZipCodesServiceTests
    {
        private readonly ZipCodesService _zipCodesService;
        public ZipCodesServiceTests()
        {
            _zipCodesService = new ZipCodesService();
        }
        [Fact]
        public void GetAllTest()
        {
            // Act
            var results = _zipCodesService.GetAll();

            //Assert
            Assert.True(results.Any());
            Assert.True(results.Count() > 0);
        }

        [Fact]
        public void GetByZipTest()
        {
            // Arrange
            var existingZip = 22222;
            var unexistingZip = 99999;

            // Act
            var result1 = _zipCodesService.GetByZip(existingZip);
            var result2 = _zipCodesService.GetByZip(unexistingZip);

            //Assert
            Assert.NotNull(result1);
            Assert.IsType<ZipCode>(result1);
            Assert.Equal(existingZip, result1.Zip);
            Assert.Null(result2);
        }

        [Fact]
        public void AddZipCodeTest()
        {
            //Arrange
            var zipCode = new ZipCode() { Zip = 44444, County= "Trumbull", City= "Newton Falls", State="OH" };

            //Act, Assert
            bool add = _zipCodesService.AddZipCode(zipCode);
            Assert.True(add);

            add = _zipCodesService.AddZipCode(zipCode);
            Assert.False(add);

        }
    }
}