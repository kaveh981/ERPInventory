using ERPInvetnory.BusinessLayer;
using ERPInvetnory.DataLayer;
using ERPInvetnory.DataLayer.Repository;
using ERPInventory.Model.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ERPInventory.App_Start
{
    public class NinjectConfig
    {
        public static Lazy<IKernel> CreateKernel = new Lazy<IKernel>(() =>
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            RegisterServices(kernel);

            return kernel;
        });

        private static void RegisterServices(KernelBase kernel)
        {
            //kernel.Bind<IOrder>().To<Mapi.BusinessLayer.Order>();
            //kernel.Bind<IItem>().To<Item>();
            kernel.Bind<ICategory>().To<Category>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            kernel.Bind<ERPInventoryDBContext>().To<ERPInventoryDBContext>();
        }
    }
}