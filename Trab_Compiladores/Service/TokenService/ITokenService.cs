using System.Collections.Generic;

namespace Trab_Compiladores.Service.TokenService
{
    public interface ITokenService
    {
            IEnumerable<string> FormatTokenString(List<TokenResult> tokens);
    }
}