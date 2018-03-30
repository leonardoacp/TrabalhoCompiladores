using System;
namespace Trab_Compiladores
{
    public enum Tag
    {
        //Operadores
        OP_EQ,
        OP_GE,
        OP_MUL,
        OP_NE,
        OP_LE,
        OP_DIV,
        OP_GT,
        OP_AD,
        OP_ASS,
        OP_LT,
        OP_MIN,

        //Símbolos
        SMB_OBC,
        SMB_COM,
        SMB_CBC,
        SMB_SEM,
        SMB_OPA,
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
