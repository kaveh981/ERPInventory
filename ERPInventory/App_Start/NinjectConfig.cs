

using ERPInventory.BusinessLayer;
using ERPInventory.DataLayer;
using ERPInventory.DataLayer.Repository;
using ERPInventory.Model.Models;
using Ninject;
using System;
using System.Reflection;
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
            kernel.Bind<ICategory>().To<Category>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            kernel.Bind<ERPInventoryDBContext>().To<ERPInventoryDBContext>();
        }
    }
}