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
    public class EkwipunekControllerTests
    {
        private ApplicationDbContext _context;
        private EkwipunekController _controller;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new EkwipunekController(_context);

            //Usuwamy z Bazy
            _context.Postacie.RemoveRange(_context.Postacie);
            _context.Ekwipunki.RemoveRange(_context.Ekwipunki);
            _context.SaveChanges();
        }

        [Test]
        public async Task Create_Ekwipunek_ValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var postacId = 1;
            var nazwa = "Miecz";
            var typ = "Broń";

            var postac = new Postac { Id = postacId, Imie = "Geralt", Poziom = 10, Umiejetnosci = "Szermierz" };
            _context.Postacie.Add(postac);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Create(postacId, nazwa, typ);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Details", redirectToActionResult.ActionName);
            Assert.AreEqual("Postac", redirectToActionResult.ControllerName);
        }
        [Test]
        public async Task Edit_Ekwipunek_ValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var postacId = 1;
            var ekwipunek = new Ekwipunek { Id = 1, Nazwa = "Miecz", Typ = "Broń", PostacId = postacId };
            _context.Ekwipunki.Add(ekwipunek);
            await _context.SaveChangesAsync();

            // Act
            ekwipunek.Nazwa = "Miecz Magiczny"; // Zmieniamy nazwę ekwipunku
            var result = await _controller.Edit(ekwipunek);  // Sprawdzamy, czy edycja przebiega prawidłowo

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Details", redirectToActionResult.ActionName);
            Assert.AreEqual("Postac", redirectToActionResult.ControllerName);
        }

      

        [Test]
        public async Task Delete_Ekwipunek_ValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var postacId = 1;
            var ekwipunek = new Ekwipunek { Id = 1, Nazwa = "Miecz", Typ = "Broń", PostacId = postacId };
            _context.Ekwipunki.Add(ekwipunek);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteConfirmed(ekwipunek.Id, postacId);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Details", redirectToActionResult.ActionName);
            Assert.AreEqual("Postac", redirectToActionResult.ControllerName);
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose(); // Zwalniamy zasoby kontrolera
            _context.Dispose(); // Zwalniamy zasoby kontekstu
        }
    }
}
