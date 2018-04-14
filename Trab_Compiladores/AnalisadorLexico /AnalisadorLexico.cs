using System;
using System.Collections.Generic;
using System.Linq;

namespace Trab_Compiladores.AnalisadorLexico
{
    public class AnalisadorLexico : IAnalisadorLexico
    {
        private readonly Service.FileService.IFileService _fileService;
        private readonly Service.TsService.ITsService _tsService;
        public  int column = 1;
        public  int line = 1;
        public int filePosition = -1;
        public string _file;
        public int state = 1;
        public System.Text.StringBuilder lexema = new System.Text.StringBuilder();
        
        public AnalisadorLexico(Service.TsService.ITsService tsService,Service.FileService.IFileService fileService){

            _fileService = fileService;
            _tsService = tsService;
        }

        public IEnumerable<TokenResult> GetTokens(string path){

            column = 1;
            line = 1;
            filePosition = -1;
            state = 1;
            
            _file = _fileService.ReadAllText(path);
            TokenResult tokenResult;
        
            do {
                tokenResult = NextToken();

                if(tokenResult.Status == true){
                    state = 1;
                    lexema = new System.Text.StringBuilder(); 
                }
                yield return tokenResult;

            } while(tokenResult?.Token?.Tag != Tag.EOF);
        }


        public void ReturnColumn(){

            filePosition--; 
            column--;
        }


        public TokenResult NextToken()
        {

        char character = ' '; 


        while(true){

        if (character == '\n')
        {
            column = 1;
            line++;
        }

        filePosition++;
        character = filePosition >= _file.Length? ' ': _file[filePosition];
        column++;

        // if (filePosition == _file.Length){
        //     return new TokenResult(true, "" , new Token(Tag.EOF, "EOF", line, column));
        // }


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
                            else if (character == '{')
                            {
                                state = 30;
                                return new TokenResult(true, "" , new Token(Tag.SMB_OBC, "{", line, column));
                            }
                            else if (character == '}')
                            {
                                state = 31;
                                return new TokenResult(true, "" , new Token(Tag.SMB_CBC, "}", line, column));
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
                            else if (character == ',')
                            {
                                state = 32;
                                return new TokenResult(true, "" , new Token(Tag.SMB_COM, ",", line, column));
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
                            else if (character == '"')
                            {
                                state = 28;
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
                                // ReturnColumn();
                                ReturnColumn();
                                state = 1;
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


                        case 29:

                            if (Char.IsLetterOrDigit(character))
                            {
                                state = 14;
                                //ReturnColumn();
                                // Permanece no state 14
                            }
                            else if(character != ' ' && character != '\n'){

                            state = 1;
                            ReturnColumn();  

                            }

                        break;    

                        case 14:

                            var tags = Enum.GetValues(typeof(Tag));
                            var tagsList = new List<string>();


                            foreach (var item in tags)
                            {
                                var description = EnumModel.GetEnumDescription((Tag)item);
                                if(!string.IsNullOrEmpty(description)){

                                    tagsList.Add(EnumModel.GetEnumDescription((Tag)item));
                                }

                            }


                            if (Char.IsLetterOrDigit(character))
                            {
                                lexema.Append(character);
                                //state = 29;
                                // Permanece no state 14
                            }


                            else if (!Char.IsLetterOrDigit(character) && !tagsList.Contains(character.ToString()) && character != ' ' && character != '\n')
                            {
                                //state = 1;
                                //ReturnColumn();
                                //state = 29;
                                // Permanece no state 14
                                var errorMessage = string.Concat("ERRO => Caractere invalido  '", character.ToString() , "'  na linha " , line , " e coluna " , column);
                                return new TokenResult(false, errorMessage , null);    
                            }



                            // else if(!Char.IsLetterOrDigit(character) && character != ' ' && character != '\n'){

                            //     var errorMessage = string.Concat("ERRO => Caractere invalido  '", character.ToString() , "'  na linha " , line , " e coluna " , column);
                            //     return new TokenResult(false, errorMessage , null);    
                            // }

                            else
                            { // state 15
                                state = 15;
                                ReturnColumn();
                                //retornaPonteiro();
                                var token = _tsService.SymbolstTable().FirstOrDefault(a => a.Lexeme.ToUpper() == lexema.ToString()?.ToUpper());

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
                            
                            state = 1;
                            ReturnColumn();
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
                            
                            state = 1;
                            ReturnColumn();
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
                                state = 1;
                                ReturnColumn();
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

                            else if(filePosition == _file.Length){
                            
                                state = 1;
                                ReturnColumn();
                            }

                            else if(character == '\n' || character == ' '){

                            }

                            else
                            {
                                var errorMessage = string.Concat("ERRO => Padrao para número invalido: \"",character.ToString(),"\" na linha " , line , " coluna " , column);
                                return new TokenResult(false, errorMessage , null);
                                //sinalizaErro("Padrao para double invalido na linha " + line + " coluna " + column);
                            }
                            break;
                        case 27:
                            if (Char.IsDigit(character))
                            {
                                lexema.Append(character);
                            }
                            else if(filePosition == _file.Length){
                                
                                state = 1;
                                ReturnColumn();
                            }
                            else
                            {
                                ReturnColumn();
                                // retornaPonteiro();
                                return new TokenResult(true, "" , new Token(Tag.CON_NUM, lexema.ToString(), line, column));
                            }
                            break;

                        case 28:
                            if (character == '"')
                            {
                                state = 25;
                                return new TokenResult(true, "" , new Token(Tag.LIT, lexema.ToString(), line, column));
                            }
                            else if (filePosition == _file.Length)
                            {
                                state = 1;
                                ReturnColumn();
                                var errorMessage = "ERRO => String deve ser fechada com => \" antes do fim de arquivo";
                                return new TokenResult(false, errorMessage , null);
                                //sinalizaErro("String deve ser fechada com \" antes do fim de arquivo");
                            }
                            else
                            { // Se vier outro, permanece no state 22
                                lexema.Append(character);
                            }
                        break;

                    }
        }

        }

    }
}

