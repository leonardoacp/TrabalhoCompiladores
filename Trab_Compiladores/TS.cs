using System;
using System.Collections.Generic;

namespace Trab_Compiladores
{
    public class TS
    {
        public readonly List<Token> _tokens;

        public TS()
        {
            _tokens = new List<Token>{

                //Palavras-chave
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
        }
    }
}
