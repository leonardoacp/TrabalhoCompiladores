using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trab_Compiladores.Service;



namespace Trab_Compiladores
{
    class Program
    {

        static void Main()
        {
            var fileService = new Service.FileService.FileService();
            var tsService = new Service.TsService.TsService();
            var tokenService = new Service.TokenService.TokenService();
            var analisadorLexico = new AnalisadorLexico.AnalisadorLexico(tsService,fileService);
            var filesDirectory = string.Concat( System.IO.Directory.GetCurrentDirectory(),"/Files/");

            var directory = new DirectoryInfo(filesDirectory);
            var Files = directory.GetFiles("*.txt").OrderBy(a => a.Name);

            foreach(FileInfo file in Files) 
            {
                var tokens = analisadorLexico.GetTokens(file.FullName).ToList();
                var tokensFormatted = tokenService.FormatTokenString(tokens);

                Console.WriteLine("");
                Console.WriteLine(string.Concat("=====> ",file.Name));
                Console.WriteLine("");


                foreach (var item in tokensFormatted)
                {
                    Console.WriteLine(item);
                }
            }

            Console.ReadLine();
        }

    }
}
