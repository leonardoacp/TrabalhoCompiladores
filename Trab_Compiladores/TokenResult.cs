namespace Trab_Compiladores
{
    public class TokenResult
    {
        private Token token;
        private string message;
        private bool status;

        public TokenResult(bool status, string message, Token token)
        {
            this.token = token;
            this.message = message;
            this.status = status;
        }

        public Token Token
        {
            get { return token; }
            set { token = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}