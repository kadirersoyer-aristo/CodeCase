using Autofac;
using BeymenCodeCase.Common;
using BeymenCodeCase.Repository;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.IoC
{
    public class AutofacIoC : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("BeymenCodeCase.Services"))
                 .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Factory") || t.Name.EndsWith("Helper"))
                 .AsImplementedInterfaces()
                 .InstancePerLifetimeScope();
        }
    }
}
