using System.Collections.Generic;
using SogetiDemo.TDD.BankApp.Common.Entities;

namespace SogetiDemo.TDD.BankApp.Services
{
    public interface IBankAccountService
    {
        /// <summary>
        /// Withdraws an amount from the account
        /// </summary>
        /// <param name="account"></param>
        /// <param name="amountToDeposit"></param>
        void WithDraw(BankAccount account, decimal amountToDeposit);

        /// <summary>
        /// Deposit an amount from the account
        /// </summary>
        /// <param name="account"></param>
        /// <param name="amountToDeposit"></param>
        void Deposit(BankAccount account, decimal amountToDeposit);

        /// <summary>
        /// Finds an account by its id.
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>The id</returns>
        BankAccount FindAccount(int id);

        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <returns></returns>
        ICollection<BankAccount> GetAllAccounts();

        /// <summary>
        /// If the amount is negative, withdraw. If the amount is positive, deposit.
        /// </summary>
        /// <param name="account">The account</param>
        /// <param name="amount">The amount</param>
        void Change(BankAccount account, decimal amount);
    }
}