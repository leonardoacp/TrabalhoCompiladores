using System;
using System.Collections.Generic;

namespace Trab_Compiladores.AnalisadorLexico
{
    public interface IAnalisadorLexico
    {
        List<string> GetTokens(string path);
        List<TokenResult> TokenValidation(string file);
        void ReturnColumn();
    }
}
