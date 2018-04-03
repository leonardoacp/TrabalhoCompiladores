using System;
using System.Collections.Generic;
using Trab_Compiladores;
using Trab_Compiladores.Service.TsService;

namespace Trab_Compiladores.Service.TsService
{
    public class TsService: ITsService
    {
        public HashSet<Token>  SymbolstTable(){

            var tokens = new HashSet<Token>(new TokenComparer());

                tokens.Add(new Token(Tag.KW, "program", 0, 0 ));
                tokens.Add(new Token(Tag.KW, "if", 0, 0 ));
                tokens.Add(new Token(Tag.KW, "else", 0, 0 ));
                tokens.Add(new Token(Tag.KW, "while", 0, 0 ));
                tokens.Add(new Token(Tag.KW, "write", 0, 0 ));
                tokens.Add(new Token(Tag.KW, "read", 0, 0 ));
                tokens.Add(new Token(Tag.KW, "num", 0, 0 ));
                tokens.Add(new Token(Tag.KW, "char", 0, 0 ));
                tokens.Add(new Token(Tag.KW, "not", 0, 0 ));
                tokens.Add(new Token(Tag.KW, "or", 0, 0 ));
                tokens.Add(new Token(Tag.KW, "and", 0, 0 ));
                

                return tokens;
        }

        private class TokenComparer : IEqualityComparer<Token>
        {
            public bool Equals(Token x, Token y)
            {
                return x.Lexeme.Equals(y.Lexeme, StringComparison.InvariantCultureIgnoreCase);
            }
         
            public int GetHashCode(Token obj)
            {
                return obj.Lexeme.GetHashCode();
            }
        }
}
}

