using Cf.Application.Interfaces;
using Cf.Application.Services.AdvertServices;
using Cf.Application.Services.Interfaces;
using Cf.Domain.Enums;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Exceptions;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Moq;
using NUnit.Framework;
using Cf.Domain.Aggregates.Adverts;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Tests.AdvertServices
{
    [TestFixture]
    public class UserAdvertServiceTests
    {
        private Mock<Context> _context;
        private Mock<IImageUploadService> _imageUploadServiceMock;
        private IUserAdvertService _sut;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<Context>();
            _imageUploadServiceMock = new Mock<IImageUploadService>();
            _sut = new UserAdvertService(_context.Object, _imageUploadServiceMock.Object);
        }

        [TestCase("Title")]
        [TestCase("Description")]
        [TestCase("Brand")]
        [TestCase("Model")]
        [TestCase("ProblemType")]
        public void CreateAsync_WhenRequiredFieldsMissing_ThrowsBadRequestException(string fieldName)
        {
            // Arrange
            var model = CreateAdvertModel();
            SetPropertyValueToNull(model, fieldName);
            string userId = "user123";

            // Act & Assert
            var ex = Assert.ThrowsAsync<BadRequestException>(() => _sut.CreateAsync(model, userId));
            Assert.That(ex.Message, Is.EqualTo(DomainErrors.Advert.OneOrMoreRequiredFlieldsUnspecified.Message));
        }

        [Test]
        public void CreateAsync_WhenUserIdFieldMissing_ThrowsBadRequestException()
        {
            // Arrange
            var model = CreateAdvertModel();

            // Act & Assert
            var ex = Assert.ThrowsAsync<BadRequestException>(() => _sut.CreateAsync(model, null));
            Assert.That(ex.Message, Is.EqualTo(DomainErrors.Advert.OneOrMoreRequiredFlieldsUnspecified.Message));
        }

        [Test]
        public void CreateAsync_WhenEndDateIsInThePast_ThrowsBadRequestException()
        {
            // Arrange
            var model = new AdvertModel
            {
                Title = "Valid Title",
                Description = "Valid Description",
                Brand = "Brand",
                Model = "Model",
                ProblemType = ProblemType.Engine.ToString(),
                EndDate = DateTime.UtcNow.AddDays(-1)
            };
            string userId = "user123";

            // Act & Assert
            var ex = Assert.ThrowsAsync<BadRequestException>(() => _sut.CreateAsync(model, userId));
            Assert.That(ex.Message, Is.EqualTo(DomainErrors.Advert.InvalidAdvertEndDate.Message));
        }

        [Test]
        public void CreateAsync_WhenTooManyImagesProvided_ThrowsBadRequestException()
        {
            // Arrange
            var model = new AdvertModel
            {
                Title = "Valid Title",
                Description = "Valid Description",
                Brand = "Brand",
                Model = "Model",
                ProblemType = ProblemType.Engine.ToString(),
                EndDate = DateTime.UtcNow.AddDays(1),
                Photos = new List<string> { "img1", "img2", "img3", "img4", "img5", "img6" }
            };
            string userId = "user123";

            // Act & Assert
            var ex = Assert.ThrowsAsync<BadRequestException>(() => _sut.CreateAsync(model, userId));
            Assert.That(ex.Message, Is.EqualTo(DomainErrors.Advert.TooMuchImages.Message));
        }

        [Test]
        public async Task CreateAsync_WhenValidModel_AddsAdvertAndReturnsAdvertIdResponse()
        {
            // Arrange
            var model = new AdvertModel
            {
                Title = "Valid Title",
                Description = "Valid Description",
                Brand = "Brand",
                Model = "Model",
                ProblemType = ProblemType.Engine.ToString(),
                EndDate = DateTime.UtcNow.AddDays(1),
                Photos = new List<string> { "img1", "img2" }
            };
            string userId = "user123";

            _imageUploadServiceMock
                .Setup(x => x.Upload(It.IsAny<string>()))
                .ReturnsAsync("http://image.url");

            _context.Setup(x => x.AddAsync(It.IsAny<Advert>(), default)).ReturnsAsync(It.IsAny<EntityEntry<Advert>>());
            _context.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _sut.CreateAsync(model, userId);

            // Assert
            _context.Verify(x => x.AddAsync(It.IsAny<Advert>(), default), Times.Once);
            _context.Verify(x => x.SaveChangesAsync(default), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Cf.Contracts.Responses.Response.AdvertIdResponse>());
        }

        private static void SetPropertyValueToNull(object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(obj, string.Empty);
            }
            else
            {
                throw new ArgumentException($"Property '{propertyName}' not found or is read-only on object of type '{obj.GetType().Name}'.");
            }
        }


        private static AdvertModel CreateAdvertModel()
        {
            return new AdvertModel
            {
                Title = "Test Title",
                ProblemType = "Engine",
                Description = "Problem with engine",
                Brand = "Toyota",
                Model = "Corolla",
                ManufactureYear = 2015,
                Photos = new List<string> { "photo1.jpg", "photo2.jpg" },
                EndDate = DateTime.Now.AddDays(7)
            };
        }
    }
}
