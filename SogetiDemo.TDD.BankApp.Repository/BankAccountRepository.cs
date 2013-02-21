using Raven.Client;
using SogetiDemo.TDD.BankApp.Common.Entities;

namespace SogetiDemo.TDD.BankApp.Repository
{
    public class BankAccountRepository : RavenDbRepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(IDocumentSession session) : base(session)
        {  
        }

        public BankAccountRepository()
        {
        }

        protected override string SessionName
        {
            get { return "BankAccount"; }
        }
    }
}
