//using PCStore.Models.Models;
//using PCStore.BL.Services;
//using Microsoft.Extensions.Logging;
//using PCStore.BL.Interfaces;
//using Xunit;
//using Moq;

//namespace ProductStore.Test
//{
//    public class StoreServiceTests
//    {
//        private readonly Mock<IProductService> _productServiceMock;
//        private readonly Mock<IManufacturerService> _manufacturerServiceMock;
//        private readonly Mock<ILogger<StoreService>> _loggerMock;
//        private readonly StoreService _storeService;

//        public StoreServiceTests()
//        {
//            _productServiceMock = new Mock<IProductService>();
//            _manufacturerServiceMock = new Mock<IManufacturerService>();
//            _loggerMock = new Mock<ILogger<StoreService>>();

//            _storeService = new StoreService(
//                _manufacturerServiceMock.Object,
//                _productServiceMock.Object,
//                _loggerMock.Object
//            );
//        }

//        [Fact]
//        public void GetFullInformation_ShouldReturnFullProductView_WhenDataExists()
//        {
//            var products = new List<Product>
//        {
//            new Product { Id = "1", ProductName = "Laptop" }
//        };

//            var manufacturers = new List<Manufacturer>
//        {
//            new Manufacturer { Id = "101", Name = "Dell" }
//        };

//            _productServiceMock
//                .Setup(service => service.GetAllProducts())
//                .Returns(products);

//            _manufacturerServiceMock
//                .Setup(service => service.GetAllManufacturers())
//                .Returns(manufacturers);

//            var result = _storeService.GetFullInformation();

//            Assert.NotNull(result);
//            Assert.Single(result);
//            Assert.Equal("Laptop", result[0].ManufacturerName);
//            Assert.Single(result[0].Products);
//            Assert.Equal("Dell", result[0].Products.First().ProductName);
//        }

//        [Fact]
//        public void GetFullInformation_ShouldReturnEmptyList_WhenNoProductsExist()
//        {
//            _productServiceMock
//                .Setup(service => service.GetAllProducts())
//                .Returns(new List<Product>());

//            var result = _storeService.GetFullInformation();

//            Assert.NotNull(result);
//            Assert.Empty(result);
//        }

//        [Fact]
//        public void GetFullInformation_ShouldReturnProductsWithEmptyManufacturers_WhenNoManufacturersExist()
//        {
//            // Arrange
//            var products = new List<Product>
//        {
//            new Product { Id = "1", ProductName = "Laptop" }
//        };

//            _productServiceMock
//                .Setup(service => service.GetAllProducts())
//                .Returns(products);

//            _manufacturerServiceMock
//                .Setup(service => service.GetAllManufacturers())
//                .Returns(new List<Manufacturer>());

//            var result = _storeService.GetFullInformation();

//            Assert.NotNull(result);
//            Assert.Single(result);
//            Assert.Empty(result[0].Products);
//        }
//    }
//}