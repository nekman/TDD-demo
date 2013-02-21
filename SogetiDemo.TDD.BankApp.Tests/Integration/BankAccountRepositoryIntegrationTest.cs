using NUnit.Framework;
using SogetiDemo.TDD.BankApp.Common.Entities;
using SogetiDemo.TDD.BankApp.Repository;
using SogetiDemo.TDD.BankApp.Services.IoC;

namespace SogetiDemo.TDD.BankApp.Tests.Integration
{
    [TestFixture]
    public class BankAccountRepositoryIntegrationTest
    {
        // System under Test
        private IBankAccountRepository _sut;
        private BankAccount _account;

        [TestFixtureSetUp]
        public void Setup()
        {
            _sut = IoC.Resolve<IBankAccountRepository>();

            _account = new BankAccount(3500) { Name = "" };
        }

        [Test]
        public void ItShouldAddAnAccount()
        {
            // Act
            _sut.Save(_account);

            // Assert
            Assert.That(_account.Id, Is.GreaterThan(0));
        }
    }
}
