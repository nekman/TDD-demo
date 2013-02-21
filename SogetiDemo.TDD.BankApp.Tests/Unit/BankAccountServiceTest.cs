using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SogetiDemo.TDD.BankApp.Common.Entities;
using SogetiDemo.TDD.BankApp.Repository;
using SogetiDemo.TDD.BankApp.Services;

namespace SogetiDemo.TDD.BankApp.Tests.Unit
{
    [TestFixture]
    class BankAccountServiceTest
    {
        // The system under test
        private IBankAccountService _sut;

        private Mock<IBankAccountRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IBankAccountRepository>();

           _sut = new BankAccountService(_repository.Object);
        }

        [Test]
        public void ItOrdersTheAccountsByBalance()
        {
            // Arrange
            var unorderedAccounts = new[]
            {
                new BankAccount(10), 
                new BankAccount(1000), 
                new BankAccount(1)
            };

            _repository.Setup(rep => rep.GetAll()).Returns(unorderedAccounts);

            // Act
            ICollection<BankAccount> orderedAccounts = _sut.GetAllAccounts();

            // Assert
            Assert.That(orderedAccounts.First().Balance, Is.EqualTo(1000));
            Assert.That(orderedAccounts.Last().Balance, Is.EqualTo(1));
        }

        [Test]
        public void ItShouldNotReturnNullValues()
        {
            // Arrange
            _repository.Setup(rep => rep.GetAll()).Returns((ICollection<BankAccount>) null);

            // Act
            ICollection<BankAccount> orderedAccounts = _sut.GetAllAccounts();

            // Assert
            Assert.That(orderedAccounts, Is.Not.Null);
        }

        [Test]
        public void ItShouldNotReturnNullWhenNoFindingAnAccount()
        {
            // Arrange
            _repository.Setup(rep => rep.GetById(It.IsAny<int>())).Returns((BankAccount) null);

            // Act
            BankAccount account = _sut.FindAccount(1234);

            // Assert
            Assert.That(account, Is.Not.Null);
            Assert.That(account, Is.EqualTo(BankAccount.Empty));
        }

        [Test]
        public void ItShouldBeAbleToWithDrawFromAnAccount()
        {
            //Arrange 
            var account = new BankAccount(balance: 1000);
            const int expected = 600;

            //Act
            _sut.WithDraw(account, 400);

            //Assert
            Assert.That(account.Balance, Is.EqualTo(expected));
            _repository.Verify(repository => repository.Save(account));
        }


        [Test]
        public void ItShouldBeAbleToDepositToAnAccount()
        {
            //Arrange 
            var account = new BankAccount(balance: 400);
            const int expected = 600;

            //Act
            _sut.Deposit(account, 200);

            //Assert
            Assert.That(account.Balance, Is.EqualTo(expected));
            _repository.Verify(repository => repository.Save(account));
        }

        [Test]
        public void ItThrowsErrorIfTheBalancIsToSmallForTheWithdraw()
        {
            var account = new BankAccount(balance: 400);

            Assert.Throws<InvalidOperationException>(() => _sut.WithDraw(account, 500));
        }

        [Test]
        public void ItThrowsErrorIfTryingToDepositAmountSmallerOrLessThanZero()
        {
            var account = new BankAccount(balance: 400);

            Assert.Throws<InvalidOperationException>(() => _sut.Deposit(account, -1));
            Assert.Throws<InvalidOperationException>(() => _sut.Deposit(account, 0));
        }

        [Test]
        public void ItThrowsErrorIfNullParametersIsUsed()
        {
            dynamic nullValue = null;

            Assert.Throws<ArgumentNullException>(() => new BankAccountService(nullValue));
            Assert.Throws<ArgumentNullException>(() => _sut.Deposit(nullValue, 10));
            Assert.Throws<ArgumentNullException>(() => _sut.WithDraw(nullValue, 10));
        }
    }
}
