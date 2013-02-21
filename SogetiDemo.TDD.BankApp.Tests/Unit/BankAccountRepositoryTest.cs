using Moq;
using NUnit.Framework;
using Raven.Client;
using SogetiDemo.TDD.BankApp.Common.Entities;
using SogetiDemo.TDD.BankApp.Repository;

namespace SogetiDemo.TDD.BankApp.Tests.Unit
{
    [TestFixture]
    class BankAccountRepositoryTest
    {
        private IBankAccountRepository _sut;
        private Mock<IDocumentSession> _session;

        [SetUp]
        public void SetUp()
        {
            _session = new Mock<IDocumentSession>();

            // System Under Test
            _sut = new BankAccountRepository(_session.Object);
        }


        [Test]
        public void ItCanStoreBankAccounts()
        {
            // Arrange
            var account = new BankAccount(balance:5000);

            // Act
            _sut.Save(account);

            // Assert
            _session.Verify(session => session.Store(account), Times.Once());
            _session.Verify(session => session.SaveChanges(), Times.Once());
        }
    }
}
