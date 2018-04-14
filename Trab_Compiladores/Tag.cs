using System;
using System.ComponentModel;

namespace Trab_Compiladores
{
    public enum Tag
    {
        //Operadores

        [Description("==")]
        OP_EQ,

        [Description(">=")]
        OP_GE,

        [Description("*")]
        OP_MUL,

        [Description("!=")]
        OP_NE,

        [Description("<=")]
        OP_LE,

        [Description("/")]
        OP_DIV,

        [Description(">")]
        OP_GT,

        [Description("+")]
        OP_AD,

        [Description("=")]
        OP_ASS,

        [Description("<")]
        OP_LT,

        [Description("-")]
        OP_MIN,

        //Símbolos

        [Description("{")]
        SMB_OBC,

        [Description(",")]
        SMB_COM,

        [Description("}")]
        SMB_CBC,

        [Description(";")]
        SMB_SEM,

        [Description("(")]
        SMB_OPA,

        [Description(")")]
        SMB_CPA,

        //Palavras-chave
        KW,

        //Identificadores
        ID,

        //Literal: 
        LIT,

        //Constantes: 
        CON_NUM,
        CON_CHAR,

        //End of file
        EOF
    }
}
