using System.Collections.Generic;
using SogetiDemo.TDD.BankApp.Common.Entities;

namespace SogetiDemo.TDD.BankApp.Repository
{
    public interface IBankAccountRepository
    {
        /// <summary>
        /// Get a bank account by its id
        /// </summary>
        /// <returns>the bank account, if found</returns>
        BankAccount GetById(dynamic id);

        /// <summary>
        /// Get all bank accounts
        /// </summary>
        /// <returns>All bank accounts</returns>
        ICollection<BankAccount> GetAll();

        /// <summary>
        /// Removes a bankaccount
        /// </summary>
        /// <param name="bankaccount">the bank account</param>
        void Remove(BankAccount bankaccount);

        /// <summary>
        /// Saves a bank account
        /// </summary>
        /// <param name="bankaccount">the bank account</param>
        void Save(BankAccount bankaccount);
    }
}
