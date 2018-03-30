using System;
using Ninject;
namespace Trab_Compiladores.Ninject
{
    public static class NinjectDependencies
    {
        public static void StartNinjectDependencies()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Service.FileService.IFileService>().To<Service.FileService.FileService>();
            kernel.Bind<AnalisadorLexico.IAnalisadorLexico>().To<AnalisadorLexico.AnalisadorLexico>();
        }
    }
}
