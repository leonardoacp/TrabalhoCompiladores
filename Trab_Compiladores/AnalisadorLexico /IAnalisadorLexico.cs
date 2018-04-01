using System;
using System.Collections.Generic;

namespace Trab_Compiladores.AnalisadorLexico
{
    public interface IAnalisadorLexico
    {
        IEnumerable<TokenResult> GetTokens(string path);
        TokenResult NextToken();
        void ReturnColumn();
    }
}
