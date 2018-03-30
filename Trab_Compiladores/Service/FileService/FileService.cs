using System;
namespace Trab_Compiladores.Service.FileService
{
    public class FileService : IFileService
    {
        public string ReadAllText(string path)
        {
            return string.IsNullOrEmpty(path) ? null: System.IO.File.ReadAllText(path);
        }
    }
}
