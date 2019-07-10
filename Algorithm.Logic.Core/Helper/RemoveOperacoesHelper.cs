using System.Text.RegularExpressions;

namespace Algorithm.Logic.Core.Helper
{
    public class RemoveOperacoesHelper
    {
        public string InputFinal { get; set; }

        public RemoveOperacoesHelper(string input)
        {
            this.InputFinal = RemoveOperacaos(input);            
        }

        /// <summary>
        /// Remove as operacoes que contém X após o "passo"
        /// </summary>
        /// <param name="input"></param>
        private string RemoveOperacaos(string input)
        {
            var retorno = input;

            while (Regex.IsMatch(retorno, @"[X]"))
            {
                var regex = new Regex(@"[SNLO]\d*?[X]");
                retorno = regex.Replace(retorno, string.Empty);
            }

            return retorno;
        }
    }
}
