namespace Trab_Compiladores
{
    public class Token
    {
        private Tag tag;
        private string lexeme;
        private int line;
        private int column;

        public Token(Tag tag, string lexeme, int line, int column)
        {

            this.tag = tag;
            this.lexeme = lexeme;
            this.line = line;
            this.column = column;
        }

        public Tag Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public string Lexeme
        {
            get { return lexeme; }
            set { lexeme = value; }
        }

        public int Line
        {
            get { return line; }
            set { line = value; }
        }

        public int Column
        {
            get { return column; }
            set { column = value; }
        }
    }

}
