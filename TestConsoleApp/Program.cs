using System;
using TestBussiness.Manager;
using StructureMap;
using TestBussiness.ManagerService;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(_ =>
            {
                _.Scan(s =>
                {
                    s.Assembly("TestBussiness");
                    s.WithDefaultConventions();
                });
                _.For<IAccountManager>().Use<AccountManager>();
            });

            var accountManager = container.GetInstance<IAccountManager>();

            var accounts = accountManager.GetAll();
        }
    }
}
