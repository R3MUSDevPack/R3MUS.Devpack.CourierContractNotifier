using Ninject;
using Ninject.Modules;
using R3MUS.Devpack.Core.AutoMapper;
using Ninject.Extensions.Conventions;
using R3MUS.Devpack.ESI;
using AutoMapper;
using System;

namespace R3MUS.Devpack.CourierContractNotifier.Ninject
{
    public class NinjectBinder : NinjectModule
    {
        private IKernel _kernel;

        public override void Load()
        {
            _kernel = this.Kernel;

            ConfigureBindings();
            ConfigureMappings();
        }

        private void ConfigureBindings()
        {
            _kernel.Load(new CoreMapperBindings());

            _kernel.Bind(x => {
                x.FromThisAssembly()
                    .SelectAllClasses()
                    .BindDefaultInterface()
                    .Configure(y => y.InThreadScope());
            });

            _kernel.Bind<ISingleSignOnService>().To<SingleSignOnService>();
        }

        private void ConfigureMappings()
        {
            var profiles = new AutoMapperProfiles();
            var types = profiles.LoadProfiles();

            Mapper.Initialize(cfg => {

                cfg.ConstructServicesUsing(t => _kernel.Get(t));

                foreach (var type in types)
                {
                    cfg.AddProfile((Profile)Activator.CreateInstance(type));
                }
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
