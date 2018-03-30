using System;
using System.Collections.Generic;

namespace Trab_Compiladores.Service.FileService
{
    public interface IFileService
    {
        string ReadAllText(string path);
    }
}
