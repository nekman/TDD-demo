using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SogetiDemo.TDD.BankApp.Repository;

namespace SogetiDemo.TDD.BankApp.Services.IoC
{
    public static class IoC
    {
        public static readonly IWindsorContainer Container = new WindsorContainer();

        static IoC()
        {
            RegisterComponents();
        }

        private static void RegisterComponents()
        {   
            // Register components. 
            // (It could also be done in XML, but thankfully we don't have to.)

            // And we can register whole assemblies by using something like:

            /*
            Container.Register(
               Classes.FromAssemblyInThisApplication().BasedOn<IBankAccountRepository>().WithService.FromInterface(),
               Classes.FromThisAssembly().BasedOn<IBankAccountService>().WithService.FromInterface()
            );
            */

            Container.Register(
                Component.For<IBankAccountRepository, BankAccountRepository>(),
                Component.For<IBankAccountService, BankAccountService>()
                    .LifeStyle
                    .PerWebRequest
            );
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
