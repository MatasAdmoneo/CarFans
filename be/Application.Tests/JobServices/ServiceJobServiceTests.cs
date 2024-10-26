using Cf.Application.Services.Interfaces;
using Cf.Application.Services.JobServices;
using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Aggregates.Jobs;
using Cf.Domain.Enums;
using Cf.Domain.Exceptions;
using Cf.Domain.Exceptions.Messages;
using Cf.Domain.Models;
using Cf.Infrastructure;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Application.Tests.JobServices
{
    [TestFixture]
    public class ServiceJobServiceTests
    {
        private Mock<Context> _context;
        private IServiceJobService _sut;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<Context>();
            _sut = new ServiceJobService(_context.Object);
        }

        [Test]
        public void CreateAsync_WhenAdvertNotFound_ThrowsBadRequestException()
        {
            // Arrange
            var advertId = Guid.NewGuid();
            _context.Setup(x => x.Adverts).ReturnsDbSet(Enumerable.Empty<Advert>());

            // Act & Assert
            var ex = Assert.ThrowsAsync<BadRequestException>(() =>
                _sut.CreateAsync(advertId, "validServiceId", new JobModel()));

            Assert.That(ex.Message, Is.EqualTo(DomainErrors.Job.AdvertNotFound.Message));
        }

        [Test]
        public void CreateAsync_WhenServiceIdIsNull_ThrowsApplicationException()
        {
            // Arrange
            var advert = CreateTestAdvert();
            _context.Setup(x => x.Adverts).ReturnsDbSet(new List<Advert>() { advert });

            // Act & Assert
            Assert.ThrowsAsync<ApplicationException>(() =>
                _sut.CreateAsync(advert.Id, null, new JobModel()));
        }

        [Test]
        public async Task CreateAsync_WhenAllParametersValid_AddsJobAndReturnsJobIdResponse()
        {
            // Arrange
            var serviceId = "validServiceId";
            var model = new JobModel { StartDate = DateTime.Now, Price = 100, Description = "Test Description" };
            var advert = CreateTestAdvert();

            _context.Setup(x => x.Adverts).ReturnsDbSet(new List<Advert>() { advert });
            _context.Setup(x => x.AddAsync(It.IsAny<Job>(), default))
                .ReturnsAsync(It.IsAny<EntityEntry<Job>>());
            _context.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _sut.CreateAsync(advert.Id, serviceId, model);

            // Assert
            _context.Verify(x => x.AddAsync(It.IsAny<Job>(), default), Times.Once);
            _context.Verify(x => x.SaveChangesAsync(default), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Status, Is.EqualTo(JobStatus.Pending));
        }

        private static Advert CreateTestAdvert()
        {
            return new Advert(
                userId: "user123",
                title: "Test Title",
                problemType: ProblemType.Engine,
                description: "Problem with engine",
                brand: "Toyota",
                model: "Corolla",
                manufactureYear: 2015,
                photos: new List<string> { "photo1.jpg", "photo2.jpg" },
                isQuestionsFormType: true,
                isSoundBad: true,
                isScentBad: false,
                isPanelInvalid: false,
                isLeakedLiquids: true,
                isUnstableCar: false,
                endDate: DateTime.Now.AddDays(7)
            );
        }
    }
}
