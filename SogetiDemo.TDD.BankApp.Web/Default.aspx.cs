using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SogetiDemo.TDD.BankApp.Common.Entities;
using SogetiDemo.TDD.BankApp.Services;
using SogetiDemo.TDD.BankApp.Services.IoC;

namespace SogetiDemo.TDD.BankApp.Web
{
    public partial class Default : Page
    {
        private readonly IBankAccountService _bankAccountService = IoC.Resolve<IBankAccountService>();

        protected void Page_Load(object sender, EventArgs e)
        {
            ICollection<BankAccount> bankAccounts = _bankAccountService.GetAllAccounts();

            BankAccountRepeater.DataSource = bankAccounts;
            BankAccountRepeater.DataBind();
        }
    }
}