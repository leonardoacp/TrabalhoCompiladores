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
        private  int column;
        private  int line;
        private int filePosition;
        
        public AnalisadorLexico(Service.FileService.IFileService fileService,Service.TokenService.ITokenService tokenService ){

            _fileService = fileService;
            _tokenService = tokenService;
            _symbolstTable = new TS()._tokens;
            column = 1;
            line = 1;
            filePosition = 0;
        }

        public List<string> GetTokens(string path){

            var tokens = new List<Token>();
            var fileText = _fileService.ReadAllText(path);
            var tokensValidate =  TokenValidation(fileText);
            return _tokenService.FormatTokenString(tokensValidate).ToList();
        }


        public void ReturnColumn(){

            filePosition--; 
            column--;
        }


        public List<TokenResult> TokenValidation(string file)
        {
            var tokens = new List<TokenResult>();
            var estado = 1;
            int previousLine = 1;
            var lexema = new System.Text.StringBuilder();  
            var newLine = false; 


            for(filePosition = 0; filePosition <= file.Length; filePosition++){
            

            if(estado == 1){
                lexema = new System.Text.StringBuilder();
            }

            var character = filePosition >= file.Length? ' ': file[filePosition];
            line = file.Take(filePosition).Count(c => c == '\n') + 1;


            if(previousLine != line){

                column = 1;
                previousLine = line;
                newLine = true;

            }
            else{
                newLine = false;
            }
            
            column ++;

                    switch (estado)
                    {


                        case 1:
                            if (filePosition == file.Length){
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.EOF, "EOF", line, column)));
                            }
                            
                            if (character == ' ' || character == '\t' || character == '\n' || character == '\r' || character == '/')
                            {
                                // Permance no estado = 1
                                if (character == '\n')
                                {

                                }
                                else if (character == '\t')
                                {

                                }

                                else if(character == '/'){

                                    estado = 28;
                                }
                            }
                            else if (Char.IsLetter(character))
                            {
                                lexema.Append(character);
                                estado = 14;
                            }
                            else if (Char.IsDigit(character))
                            {
                                lexema.Append(character);
                                estado = 12;
                            }
                            else if (character == '<')
                            {
                                estado = 6;
                            }
                            else if (character == '>')
                            {
                                estado = 9;
                            }
                            else if (character == '=')
                            {
                                estado = 2;
                            }
                            else if (character == '!')
                            {
                                estado = 4;
                            }
                            else if (character == '/')
                            {
                                estado = 16;
                            }
                            else if (character == '*')
                            {
                                estado = 1;
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.OP_MUL, "*", line, column)));
                            }
                            else if (character == '+')
                            {
                                estado = 1;
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.OP_AD, "+", line, column)));
                            }
                            else if (character == '-')
                            {
                                estado = 1;
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.OP_MIN, "-", line, column)));
                            }
                            else if (character == ';')
                            {
                                estado = 1;
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.SMB_SEM, ";", line, column)));
                            }
                            else if (character == '(')
                            {
                                estado = 1;
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.SMB_OPA, "(", line, column)));
                            }
                            else if (character == ')')
                            {
                                estado = 1;
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.SMB_CPA, ")", line, column)));
                            }
                            else if (character == '\'')
                            {
                                estado = 24;
                            }
                            else
                            {
                                var errorMessage = string.Concat("ERRO => Caractere invalido  '", character.ToString() , "'  na linha " , line , " e coluna " , column);
                                tokens.Add(new TokenResult(false, errorMessage , null));
                                return tokens;
                            }
                        break;
                        case 2:
                            if (character == '=')
                            { // Estado 3
                                estado = 1;
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.OP_EQ, "==", line, column)));
                            }
                            else
                            {
                                ReturnColumn();
                                //retornaPonteiro();
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.OP_ASS, "=", line, column)));
                            }
                            break;
                        case 4:
                            if (character == '=')
                            { // Estado 5
                                estado = 1;
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.OP_NE, "!=", line, column)));
                            }
                            else
                            {
                                ReturnColumn();
                                //retornaPonteiro();
                                var errorMessage = string.Concat("ERRO => Token incompleto para o caractere ! na linha " , line , " e coluna " , column);
                                tokens.Add(new TokenResult(false, errorMessage , null));
                                return tokens;
                                // sinalizaErro("Token incompleto para o caractere ! na linha " + line + " e coluna " + column);
                            }
                            break;
                        case 6:
                            if (character == '=')
                            { // Estado 7
                                estado = 1;
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.OP_LE, "<=", line, column)));
                            }
                            else
                            { // Estado 8
                                estado = 1;
                                ReturnColumn();
                                //retornaPonteiro();
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.OP_LT, "<", line, column)));
                            }
                            break;
                        case 9:
                            if (character == '=')
                            { // Estado 10
                                estado = 1;
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.OP_GE, ">=", line, column)));
                            }
                            else
                            { // Estado 11
                                estado = 1;
                                ReturnColumn();
                                // retornaPonteiro();
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.OP_GT, ">", line, column)));
                            }
                            break;
                        case 12:
                            if (Char.IsDigit(character))
                            {
                                lexema.Append(character);
                                // Permanece no estado 12
                            }
                            else if (character == '.')
                            {
                                lexema.Append(character);
                                estado = 26;
                            }
                            else
                            { // Estado 13
                                estado = 1;
                                ReturnColumn();
                                // retornaPonteiro();
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.CON_NUM, lexema.ToString(), line, column)));
                            }
                            break;
                        case 14:

                            if (Char.IsLetterOrDigit(character) || character == '_')
                            {
                                lexema.Append(character);
                                // Permanece no estado 14
                            }
                            else
                            { // Estado 15
                                estado = 1;
                                ReturnColumn();
                                //retornaPonteiro();
                                var token = _symbolstTable.FirstOrDefault(a => a.Lexeme.ToUpper() == lexema.ToString()?.ToUpper());

                                if (token == null){
                                    tokens.Add(new TokenResult(true, "" , new Token(Tag.ID, lexema.ToString(), line, column)));
                                }

                                else{
                                    tokens.Add(new TokenResult(true, "" , new Token(token.Tag, token.Lexeme.ToString(), line, column)));
                                }
                            }
                            break;
                        case 16:
                            if (character == '/')
                            {
                                estado = 17;
                            }
                            else
                            {
                                ReturnColumn();
                                //retornaPonteiro();
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.OP_DIV, lexema.ToString(), line, column)));
                            }
                            break;
                        case 17:
                            if (character == '\n')
                            {

                            }
                            // Se vier outro, permanece no estado 17
                            break;
                        case 24:
                            if (character == '\'')
                            {
                                estado = 25;
                                tokens.Add(new TokenResult(true, "" , new Token(Tag.CON_CHAR, lexema.ToString(), line, column)));
                            }
                            else if (filePosition == file.Length)
                            {
                                var errorMessage = "String deve ser fechada com => \'  antes do fim de arquivo";
                                tokens.Add(new TokenResult(false, errorMessage , null));
                                //sinalizaErro("String deve ser fechada com \" antes do fim de arquivo");
                                return tokens;
                            }
                            else
                            { // Se vier outro, permanece no estado 24
                                lexema.Append(character);
                            }
                            break;
                        case 26:
                            if (Char.IsDigit(character))
                            {
                                lexema.Append(character);
                                estado = 27;
                            }
                            else
                            {
                                var errorMessage = string.Concat("ERRO => Padrao para double invalido na linha " , line , " coluna " , column);
                                tokens.Add(new TokenResult(false, errorMessage , null));
                                return tokens;
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

                        case 29:

                        if(newLine == true){

                            estado = 1;
                            ReturnColumn();
                        }

                        break;

                        case 28:

                        if(character == '*'){

                        }

                        else if(character == '/' && file[filePosition-1] == '/'){
                            estado = 29;
                        }

                        else if(character == '/'){
                            estado = 1;
                        }
                        
                        else if(filePosition == file.Length){

                            var errorMessage = "ERRO => O comentário deve terminar com “*/”";
                            tokens.Add(new TokenResult(false, errorMessage , null));
                            return tokens;
                        }

                        break;

                    default:

                        estado = 1;

                        break;
                    }
                
            }

            return tokens;

        }


    }
}

