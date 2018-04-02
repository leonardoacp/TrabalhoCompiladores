using System;
using System.Linq;
using Trab_Compiladores.Service;



namespace Trab_Compiladores
{
    class Program
    {

        static void Main()
        {
            var fileService = new Service.FileService.FileService();
            var tokenService = new Service.TokenService.TokenService();
            var analisadorLexico = new AnalisadorLexico.AnalisadorLexico(fileService,tokenService);
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(location), "Files/OK/pasc_1.txt");

            var tokens = analisadorLexico.GetTokens(path).ToList();
            var tokensFormatted = tokenService.FormatTokenString(tokens);


            foreach (var item in tokensFormatted)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }

    }
}
