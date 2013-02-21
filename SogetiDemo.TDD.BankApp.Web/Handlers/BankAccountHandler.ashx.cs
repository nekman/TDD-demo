using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using SogetiDemo.TDD.BankApp.Common.Entities;
using SogetiDemo.TDD.BankApp.Services;
using SogetiDemo.TDD.BankApp.Services.IoC;

namespace SogetiDemo.TDD.BankApp.Web.Handlers
{
    public class BankAccountHandler : IHttpHandler
    {
        private readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();

        private readonly IBankAccountService _bankAccountService;
        
        public BankAccountHandler(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        public BankAccountHandler() : this(IoC.Resolve<IBankAccountService>())
        {            
        }

        public void ProcessRequest(HttpContext context)
        {
            using (var inputStream = new StreamReader(context.Request.InputStream))
            {
                string json = inputStream.ReadToEnd();
                var jsonAccount = _serializer.Deserialize<JsonAccount>(json);

                ProcessRequest(new HttpContextWrapper(context), jsonAccount);
            }
        }

        public void ProcessRequest(HttpContextBase context, JsonAccount jsonAccount)
        {
            HttpResponseBase response = context.Response;
            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            
            try
            {
                HandleChangeAmount(jsonAccount);
            }
            catch (Exception e)
            {
                jsonAccount.ErrorMessage = e.Message;
            }

            response.Write(_serializer.Serialize(jsonAccount));
          }

        private void HandleChangeAmount(JsonAccount jsonAccount)
        {
            BankAccount account = _bankAccountService.FindAccount(jsonAccount.Id);

            _bankAccountService.Change(account, jsonAccount.Amount);

            jsonAccount.Balance = account.Balance;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}