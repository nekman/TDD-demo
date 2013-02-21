using System.Web;
using Moq;
using NUnit.Framework;
using SogetiDemo.TDD.BankApp.Common.Entities;
using SogetiDemo.TDD.BankApp.Repository;
using SogetiDemo.TDD.BankApp.Services;
using SogetiDemo.TDD.BankApp.Web.Handlers;

namespace SogetiDemo.TDD.BankApp.Tests.Unit
{
    [TestFixture]
    class BankAccountHandlerTest
    {
        private readonly Mock<IBankAccountRepository> _repository = new Mock<IBankAccountRepository>();
        private readonly Mock<HttpContextBase> _httpContext = new Mock<HttpContextBase>();
        private readonly Mock<HttpResponseBase> _httpResponse = new Mock<HttpResponseBase>();

        private BankAccountHandler _sut;
        private BankAccount _account;

        [SetUp]
        public void Setup()
        {
            _httpContext.Setup(ctx => ctx.Response).Returns(_httpResponse.Object);

            _sut = new BankAccountHandler(new BankAccountService(_repository.Object));

             _account = new BankAccount(1000) { Id = 1 };
             _repository.Setup(rep => rep.GetById(It.IsAny<int>())).Returns(_account);
        }

        [Test]
        public void ShouldBeAbleToDeposit()
        {
            // Arrange
            var jsonAccount = new JsonAccount { Amount = 1000, Id = 1 };

            // Act
            _sut.ProcessRequest(_httpContext.Object, jsonAccount);

            // Assert
            _repository.Verify(rep => rep.Save(_account));
            Assert.That(_account.Balance, Is.EqualTo(2000));
            Assert.That(_account.Balance, Is.EqualTo(jsonAccount.Balance));
        }

        [Test]
        public void ShouldBeAbleToWithDraw()
        {
            // Arrange
            var jsonAccount = new JsonAccount { Amount = -1000, Id = 1 };

            // Act
            _sut.ProcessRequest(_httpContext.Object, jsonAccount);

            // Assert
            _repository.Verify(rep => rep.Save(_account));
            Assert.That(_account.Balance, Is.EqualTo(0));
            Assert.That(_account.Balance, Is.EqualTo(jsonAccount.Balance));
        }

        [Test]
        public void ShouldContainErrorMessage()
        {
            // Arrange
            var jsonAccount = new JsonAccount { Amount = -100000, Id = 1 };

            // Act
            _sut.ProcessRequest(_httpContext.Object, jsonAccount);

            // Assert
            _repository.Verify(rep => rep.Save(_account), Times.Never());

            Assert.That(jsonAccount.HasError, Is.True);
            Assert.That(jsonAccount.ErrorMessage, Is.EqualTo("The balance cannot be smaller than the amount to withdraw!"));
        }
    }
}
