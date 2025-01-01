using Moq;
using NUnit.Framework;
using TheWitcher.Controllers;
using TheWitcher.Data;
using TheWitcher.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TheWitcher.Data.Data;

namespace TheWitcher.Tests
{
    public class PostacControllerTests
    {
        private ApplicationDbContext _context;
        private PostacController _controller;

        [SetUp]
        public void SetUp()
        {
            // In-memory database setup
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new PostacController(_context);

            //Usuwamy z bazy
            _context.Postacie.RemoveRange(_context.Postacie);
            _context.SaveChanges();
        }

        [Test]
        public async Task Create_Postac_ValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var postac = new Postac
            {
                Imie = "Geralt",
                Poziom = 10,
                Umiejetnosci = "Szermierz",
                RasaId = 1
            };

            // Act
            var result = await _controller.Create(postac);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [Test]
        public async Task Edit_Get_InvalidPostacId_ReturnsNotFound()
        {
            // Arrange
            var postac = new Postac
            {
                Id = 1,
                Imie = "Geralt",
                Poziom = 10,
                Umiejetnosci = "Szermierz",
                RasaId = 1
            };

            _context.Postacie.Add(postac);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Edit(2); // Szukamy innej postaci (o id 2)

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Delete_Postac_ValidId_RemovesPostacAndRedirectsToIndex()
        {
            // Arrange
            var postac = new Postac
            {
                Id = 1,
                Imie = "Geralt",
                Poziom = 10,
                Umiejetnosci = "Szermierz",
                RasaId = 1
            };
            _context.Postacie.Add(postac);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Index", (result as RedirectToActionResult)?.ActionName);
            Assert.IsNull(await _context.Postacie.FindAsync(1)); // Ensure  postac jest usuniety
        }

        [TearDown]
        public void TearDown()
        {
            //Zwalniamy zasoby 
            _controller.Dispose();
            _context.Dispose();
        }
    }
}
