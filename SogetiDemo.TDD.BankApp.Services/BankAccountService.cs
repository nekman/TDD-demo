using System;
using System.Collections.Generic;
using System.Linq;
using SogetiDemo.TDD.BankApp.Common.Entities;
using SogetiDemo.TDD.BankApp.Common.Utils;
using SogetiDemo.TDD.BankApp.Repository;

namespace SogetiDemo.TDD.BankApp.Services
{
    /// <summary>
    /// The BankAccountService - contains all logic related to bankaccount operations.
    /// </summary>
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _repository;

        public BankAccountService(IBankAccountRepository repository)
        {
            Assert.NotNull(repository);

            _repository = repository;
        }

        public void WithDraw(BankAccount account, decimal amountToWithdraw)
        {
            ValidateWithDrawAccount(account, amountToWithdraw);

            account.WithDraw(amountToWithdraw);

            _repository.Save(account);
        }

        public void Deposit(BankAccount account, decimal amountToDeposit)
        {
            ValidateDepositAccount(account, amountToDeposit);

            account.Deposit(amountToDeposit);

            _repository.Save(account);
        }

        public BankAccount FindAccount(int id)
        {
            return _repository.GetById(id) ?? BankAccount.Empty;
        }

        public ICollection<BankAccount> GetAllAccounts()
        {
            IEnumerable<BankAccount> accounts = _repository.GetAll() ?? Enumerable.Empty<BankAccount>();

            return accounts
                .OrderByDescending(account => account.Balance)
                .ToList();
        }

        public void Change(BankAccount account, decimal amount)
        {
            if (amount < 0)
            {
                WithDraw(account, Math.Abs(amount));
            }
            else
            {
                Deposit(account, amount);
            }
        }

        private static void ValidateDepositAccount(BankAccount account, decimal amountToDeposit)
        {
            Assert.NotNull(account);

            ValidateBiggerThanZero(amountToDeposit);
        }

        private static void ValidateWithDrawAccount(BankAccount account, decimal amountToWithDraw)
        {
            Assert.NotNull(account);

            ValidateBiggerThanZero(amountToWithDraw);

            if (account.Balance < amountToWithDraw)
            {
                throw new InvalidOperationException("The balance cannot be smaller than the amount to withdraw!");
            }
        }

        private static void ValidateBiggerThanZero(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("The Amount must be greater than zero");
            }

        } 
    }
}