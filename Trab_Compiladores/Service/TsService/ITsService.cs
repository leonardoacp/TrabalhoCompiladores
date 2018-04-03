using System.Collections.Generic;

namespace Trab_Compiladores.Service.TsService

{
    public interface ITsService
    {
        HashSet<Token>  SymbolstTable();
    }
}