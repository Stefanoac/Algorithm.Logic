using Algorithm.Logic.Core.Helper;
using Algorithm.Logic.Shared.Interface;
using Algorithm.Logic.Shared.Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Algorithm.Logic.Core
{
    public class Processamento : IProcessamento
    {

        public Processamento()
        {
        }

        public string Processar(string input)
        {
            // Valida se é null ou empty ou espaço vazio
            if (!string.IsNullOrWhiteSpace(input) || !string.IsNullOrEmpty(input))
            {
                //Realiza as validações necessárias.
                if (ValidaCoordenadas(input))
                {
                    return string.Format(@"({0}, {1})", 999, 999);
                }
            }
            else
            {
                return string.Format(@"({0}, {1})", 999, 999);
            }

            var cordenadasFinais = new RemoveOperacoesHelper(input);

            // cria novo objeto ja com o input recebido
            var coordenadas = new Coordenadas()
            {
                Input = cordenadasFinais.InputFinal.ToUpper()
            };

            //pego todos os passos encontrados
            var regex = new Regex(@"[SNLO]\d*");
            var passos = regex.Matches(coordenadas.Input);

            //passo um a um, e insiro a cordenada no objeto, deslocando o X e o Y conforme o passo
            foreach (var passo in passos)
            {
                var atributos = new Regex(@"[SNLO]|\d+").Matches(passo.ToString());
                var p = atributos[0].ToString();
                var v = atributos.Count > 1 ? int.Parse(atributos[1].ToString()) : 1;

                coordenadas.AlteraCoordenada(p, v);
            }

            return $"({coordenadas.X}, {coordenadas.Y})";
        }

        /// <summary>
        /// Valida se tem X com digito depois
        /// Valida se veio apenas cordenadas corretas (n, s, l, o, x)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool ValidaCoordenadas(string input)
        {
            return (Regex.IsMatch(input, @"[X]\d")
                || Regex.IsMatch(input, @"[^SNLOX0-9]")
                || Regex.IsMatch(input, @"^\d")
                || !Regex.IsMatch(input, @"[A-Z]([1-9]|1\d{1,9}|20\d{8}|213\d{7}|2146\d{6}|21473\d{5}|214747\d{4}|2147482\d{3}|21474835\d{2}|214748364[0-6])?[A-Z]"));
        }
    }
}
