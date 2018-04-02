using System;
using System.Collections.Generic;
using System.Linq;

namespace Trab_Compiladores.AnalisadorLexico
{
    public class AnalisadorLexico : IAnalisadorLexico
    {
        private readonly Service.FileService.IFileService _fileService;
        private readonly Service.TokenService.ITokenService _tokenService;
        private readonly List<Token> _symbolstTable;
        public  int column = 1;
        public  int line = 1;
        public int filePosition = 0;
        public string _file;
        
        public AnalisadorLexico(Service.FileService.IFileService fileService,Service.TokenService.ITokenService tokenService ){

            _fileService = fileService;                                 
            _tokenService = tokenService;
            _symbolstTable = new TS().SymbolstTable();

        }

        public IEnumerable<TokenResult> GetTokens(string path){

            column = 1;
            line = 1;
            filePosition = 0;
            
            _file = _fileService.ReadAllText(path);
            TokenResult tokenResult;
        
            do {
                tokenResult = NextToken();
                yield return tokenResult;
            } while(tokenResult?.Token?.Tag != Tag.EOF && tokenResult.Status == true);
        }


        public void ReturnColumn(){

            filePosition--; 
            column--;
        }


        public TokenResult NextToken()
        {
            var state = 1;
            var lexema = new System.Text.StringBuilder(); 
            char character = ' '; 


        while(true){

        if (character == '\n')
        {
            column = 1;
            line++;
        }

            character = filePosition > _file.Length? ' ': _file[filePosition];
            filePosition++;
            column++;

                    switch (state)
                    {
                        case 1:
                            if (filePosition == _file.Length){
                                return new TokenResult(true, "" , new Token(Tag.EOF, "EOF", line, column));
                            }
                            
                            if (character == ' ' || character == '\t' || character == '\n' || character == '\r')
                            {
                                if (character == '\t')
                                {

                                }
                            }
                            else if (Char.IsLetter(character))
                            {
                                lexema.Append(character);
                                state = 14;
                            }
                            else if (Char.IsDigit(character))
                            {
                                lexema.Append(character);
                                state = 12;
                            }
                            else if (character == '<')
                            {
                                state = 6;
                            }
                            else if (character == '>')
                            {
                                state = 9;
                            }
                            else if (character == '=')
                            {
                                state = 2;
                            }
                            else if (character == '!')
                            {
                                state = 4;
                            }
                            else if (character == '/')
                            {
                                state = 16;
                            }
                            else if (character == '*')
                            {
                                state = 18;
                                return new TokenResult(true, "" , new Token(Tag.OP_MUL, "*", line, column));
                            }
                            else if (character == '+')
                            {
                                state = 19;
                                return new TokenResult(true, "" , new Token(Tag.OP_AD, "+", line, column));
                            }
                            else if (character == '-')
                            {
                                state = 20;
                                return new TokenResult(true, "" , new Token(Tag.OP_MIN, "-", line, column));
                            }
                            else if (character == ';')
                            {
                                state = 21;
                                return new TokenResult(true, "" , new Token(Tag.SMB_SEM, ";", line, column));
                            }
                            else if (character == '(')
                            {
                                state =22;
                                return new TokenResult(true, "" , new Token(Tag.SMB_OPA, "(", line, column));
                            }
                            else if (character == ')')
                            {
                                state = 23;
                                return new TokenResult(true, "" , new Token(Tag.SMB_CPA, ")", line, column));
                            }
                            else if (character == '\'')
                            {
                                state = 24;
                            }
                            else
                            {
                                var errorMessage = string.Concat("ERRO => Caractere invalido  '", character.ToString() , "'  na linha " , line , " e coluna " , column);
                                return new TokenResult(false, errorMessage , null);
                            }
                        break;
                        case 2:
                            if (character == '=')
                            { // state 3
                                state = 3;
                                return new TokenResult(true, "" , new Token(Tag.OP_EQ, "==", line, column));
                            }
                            else
                            {
                                ReturnColumn();
                                //retornaPonteiro();
                                return new TokenResult(true, "" , new Token(Tag.OP_ASS, "=", line, column));
                            }
                        
                        case 4:
                            if (character == '=')
                            { // state 5
                                state = 5;
                                return new TokenResult(true, "" , new Token(Tag.OP_NE, "!=", line, column));
                            }
                            else
                            {
                                ReturnColumn();
                                //retornaPonteiro();
                                var errorMessage = string.Concat("ERRO => Token incompleto para o caractere ! na linha " , line , " e coluna " , column);
                                return new TokenResult(false, errorMessage , null);

                                // sinalizaErro("Token incompleto para o caractere ! na linha " + line + " e coluna " + column);
                            }
                        
                        case 6:
                            if (character == '=')
                            { // state 7
                                state = 7;
                                return new TokenResult(true, "" , new Token(Tag.OP_LE, "<=", line, column));
                            }
                            else
                            { // state 8
                                state = 8;
                                ReturnColumn();
                                //retornaPonteiro();
                                return new TokenResult(true, "" , new Token(Tag.OP_LT, "<", line, column));
                            }
                        
                        case 9:
                            if (character == '=')
                            { // state 10
                                state = 10;
                                return new TokenResult(true, "" , new Token(Tag.OP_GE, ">=", line, column));
                            }
                            else
                            { // state 11
                                state = 11;
                                ReturnColumn();
                                // retornaPonteiro();
                                return new TokenResult(true, "" , new Token(Tag.OP_GT, ">", line, column));
                            }
                        
                        case 12:
                            if (Char.IsDigit(character))
                            {
                                lexema.Append(character);
                                // Permanece no state 12
                            }
                            else if (character == '.')
                            {
                                lexema.Append(character);
                                state = 26;
                            }
                            else
                            { // state 13
                                state = 13;
                                ReturnColumn();
                                // retornaPonteiro();
                                return new TokenResult(true, "" , new Token(Tag.CON_NUM, lexema.ToString(), line, column));
                            }
                            break;
                        case 14:

                            if (Char.IsLetterOrDigit(character) || character == '_')
                            {
                                lexema.Append(character);
                                // Permanece no state 14
                            }
                            else
                            { // state 15
                                state = 15;
                                ReturnColumn();
                                //retornaPonteiro();
                                var token = _symbolstTable.FirstOrDefault(a => a.Lexeme.ToUpper() == lexema.ToString()?.ToUpper());

                                if (token == null){
                                    return new TokenResult(true, "" , new Token(Tag.ID, lexema.ToString(), line, column));
                                }

                                else{
                                    return new TokenResult(true, "" , new Token(token.Tag, token.Lexeme.ToString(), line, column));
                                }
                            }
                            break;
                        case 16:

                            if (character == '/') {
                                state = 19;
                            }
                            
                            else if (character == '*')
                            {
                                state = 17;
                            }
                            else
                            {
                                ReturnColumn();
                                //retornaPonteiro();
                                return new TokenResult(true, "" , new Token(Tag.OP_DIV, "/", line, column));
                            }
                            break;

                        case 17:

                            if (character == '*')
                            {
                                state = 18;
                            }

                            else if(filePosition == _file.Length){
                            
                            var errorMessage = "ERRO => O comentário deve terminar com “*/”";
                            return new TokenResult(false, errorMessage , null);

                            }
                            
                            // Se vier outro, permanece no state 17
                            break;

                        case 18:
                        if(character == '/'){

                            state = 1;
                            
                        }
                        
                        else if(filePosition == _file.Length){
                            
                            var errorMessage = "ERRO => O comentário deve terminar com “*/”";
                            return new TokenResult(false, errorMessage , null);
                        }

                        else{

                            state = 17;
                        }

                        break;   


                        case 19:

                            if (character == '\n') {

                                state = 1;
                            } 

                        break;    
                    
                        case 24:
                            if (character == '\'')
                            {
                                state = 25;
                                return new TokenResult(true, "" , new Token(Tag.CON_CHAR, lexema.ToString(), line, column));
                            }
                            else if (filePosition == _file.Length)
                            {
                                var errorMessage = "ERRO => String deve ser fechada com => \'  antes do fim de arquivo";
                                return new TokenResult(false, errorMessage , null);
                                //sinalizaErro("String deve ser fechada com \" antes do fim de arquivo");
                            }
                            else
                            { // Se vier outro, permanece no state 24
                                lexema.Append(character);
                            }
                            break;
                        case 26:
                            if (Char.IsDigit(character))
                            {
                                lexema.Append(character);
                                state = 27;
                            }
                            else
                            {
                                var errorMessage = string.Concat("ERRO => Padrao para double invalido na linha " , line , " coluna " , column);
                                return new TokenResult(false, errorMessage , null);
                                //sinalizaErro("Padrao para double invalido na linha " + line + " coluna " + column);
                            }
                            break;
                        case 27:
                            if (Char.IsDigit(character))
                            {
                                lexema.Append(character);
                            }
                            else
                            {
                                ReturnColumn();
                                // retornaPonteiro();
                                // return new Token(Tag.DOUBLE, lexema.ToString(), line, column);
                            }
                            break;

                    }
        }

        }

    }
}

