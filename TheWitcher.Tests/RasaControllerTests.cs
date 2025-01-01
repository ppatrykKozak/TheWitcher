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
    public class RasaControllerTests
    {
        private ApplicationDbContext _context;
        private RasaController _controller;

        [SetUp]
        public void SetUp()
        {
            // Tworzenie pamięciowego kontekstu bazy danych
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")  // Używamy in-memory DB do testów
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new RasaController(_context);

            // Czyszczenie bazy danych przed każdym testem
            _context.Rasy.RemoveRange(_context.Rasy);
            _context.SaveChanges();
        }

        // Test dla metody Create (GET)
        [Test]
        public async Task Create_Rasa_ValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var rasa = new Rasa { Nazwa = "Elf", Opis = "Elfy z lasów" };

            // Act
            var result = await _controller.Create(rasa);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);  // Przekierowanie do Index
        }

        // Test dla metody Edit (GET) - Edycja istniejącej rasy
        [Test]
        public async Task Edit_Rasa_ValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var rasa = new Rasa { Nazwa = "Elf", Opis = "Elfy z lasów" };
            _context.Rasy.Add(rasa);
            await _context.SaveChangesAsync();

            // Zmieniamy nazwę rasy
            rasa.Nazwa = "Człowiek";

            // Act
            var result = await _controller.Edit(rasa);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);  // Po zapisaniu zmiany przekierowanie do Index
        }

        // Test dla metody Delete (GET)
        [Test]
        public async Task Delete_Rasa_ValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var rasa = new Rasa { Nazwa = "Elf", Opis = "Elfy z lasów" };
            _context.Rasy.Add(rasa);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteConfirmed(rasa.Id);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);  // Po usunięciu przekierowanie do Index
        }

        //// Test dla metody Edit (GET) - Wyszukiwanie nieistniejącej rasy
        //[Test]
        //public async Task Edit_Rasa_InvalidModel_ReturnsNotFound()
        //{
        //    // Act
        //    var result = await _controller.Edit(999); // Id rasy, która nie istnieje

        //    // Assert
        //    Assert.IsInstanceOf<NotFoundResult>(result);  // Zwrócenie odpowiedzi NotFound
        //}

        [TearDown]
        public void TearDown()
        {
            // Zwalnianie zasobów po każdym teście
            _controller.Dispose();
            _context.Dispose();
        }
    }
}
