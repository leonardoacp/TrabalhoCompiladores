using System.Collections.Generic;

namespace Trab_Compiladores.Service.TokenService
{
    public class TokenService : ITokenService
    {
        public IEnumerable<string> FormatTokenString(List<TokenResult> tokens){

                    foreach(var item in tokens){

                        yield return item.Status ? 
                        string.Concat("Token: <",item.Token.Tag.ToString(),":'",item.Token.Lexeme,"'> ", "Linha: ",item.Token.Line," Coluna: ",item.Token.Column):
                        string.Concat(item.Message);
                    }
        }
    }
}
