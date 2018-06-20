using Ninject;
using R3MUS.Devpack.CourierContractNotifier.Ninject;
using R3MUS.Devpack.CourierContractNotifier.Services;
using System;

namespace R3MUS.Devpack.CourierContractNotifier
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new NinjectBinder());
            var workerService = kernel.Get<IContractNotificationService>();
            workerService.RunWorker();
        }
    }
}
