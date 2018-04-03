using System;
using System.Collections.Generic;
using Trab_Compiladores;
using Trab_Compiladores.Service.TsService;

namespace Trab_Compiladores.Service.TsService
{
    public class TsService: ITsService
    {
        public HashSet<Token>  SymbolstTable(){

            var tokens = new Token[] {
                new Token(Tag.KW, "program", 0, 0 ),
                new Token(Tag.KW, "if", 0, 0 ),
                new Token(Tag.KW, "else", 0, 0 ),
                new Token(Tag.KW, "while", 0, 0 ),
                new Token(Tag.KW, "write", 0, 0 ),
                new Token(Tag.KW, "read", 0, 0 ),
                new Token(Tag.KW, "num", 0, 0 ),
                new Token(Tag.KW, "char", 0, 0 ),
                new Token(Tag.KW, "not", 0, 0 ),
                new Token(Tag.KW, "or", 0, 0 ),
                new Token(Tag.KW, "and", 0, 0 ),
            };
            
            return new HashSet<Token>(tokens);
        }
    }
}
