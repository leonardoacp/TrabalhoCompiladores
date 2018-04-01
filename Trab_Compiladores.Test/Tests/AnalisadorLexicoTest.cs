using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Trab_Compiladores.Test.Tests
{
    public class AnalisadorLexicoTest
    {
        [Theory]
        [InlineData(2,2,1,1)]
        [InlineData(4,5,3,4)]
        [InlineData(7,8,6,7)]
        [InlineData(9,10,8,9)]
        [InlineData(10,7,9,6)]
        public void FilePositionAndColumnShouldDecrease(int filePosition, int column, int filePositionExpected,int columnExpected)
        {
            //Arrange
            var fileService = new Service.FileService.FileService();
            var tokenService = new Service.TokenService.TokenService();
            var analisadorLexico = new Trab_Compiladores.AnalisadorLexico.AnalisadorLexico(fileService,tokenService);
            analisadorLexico.filePosition = filePosition;
            analisadorLexico.column = column;


            //Act
            analisadorLexico.ReturnColumn();


            //Assert
            Assert.True(analisadorLexico.filePosition == filePositionExpected && analisadorLexico.column == columnExpected);
        }

        [Fact]
        public void TokensShouldReturnFormattedWhenErrorIsFalse(){

            //Arrange
            var tokenService = new Service.TokenService.TokenService();
            var tokensResult = new List<TokenResult>{
                new TokenResult(true,"var",new Token(Tag.ID,"var",6,2)),
                new TokenResult(true,"public",new Token(Tag.KW,"public",1,2)),
                new TokenResult(true,"while",new Token(Tag.KW,"while",3,4))
            };


            //Act
            var tokenFormatted = tokenService.FormatTokenString(tokensResult).ToList();


            //Assert
            foreach (var item in tokensResult.Select((value, index) => new { value, index })){
                Assert.Equal(tokenFormatted[item.index], string.Concat("Token: <",item.value.Token.Tag.ToString(),":'",item.value.Token.Lexeme,"'> ", "Linha: ",item.value.Token.Line," Coluna: ",item.value.Token.Column));
            }

        }

        [Fact]
        public void TokensShouldReturnFormattedWhenErrorIsTrue(){

            //Arrange
            var tokenService = new Service.TokenService.TokenService();
            var tokensResult = new List<TokenResult>{
                new TokenResult(false,"",null),
                new TokenResult(false,"",null),
                new TokenResult(false,"",null)
            };


            //Act
            var tokenFormatted = tokenService.FormatTokenString(tokensResult).ToList();


            //Assert
            foreach (var item in tokensResult.Select((value, index) => new { value, index })){
                Assert.Equal(tokenFormatted[item.index],item.value.Message);
            }
        }

        [Fact]
        public void TokensShouldReturn(){

            var fileService = new Service.FileService.FileService();
            var tokenService = new Service.TokenService.TokenService();
            var analisadorLexico = new Trab_Compiladores.AnalisadorLexico.AnalisadorLexico(fileService,tokenService);





        }
    }

}

