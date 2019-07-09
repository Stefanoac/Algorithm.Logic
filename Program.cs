
namespace Algorithm.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Program
    {
        private static readonly int _maxNumber = 2147483647;
        /// <summary>
        /// PROBLEMA:
        /// 
        /// Implementar um algoritmo para o controle de posição de um drone emum plano cartesiano (X, Y).
        /// 
        /// O ponto inicial do drone é "(0, 0)" para cada execução do método Evaluate ao ser executado cada teste unitário.
        /// 
        /// A string de entrada pode conter os seguintes caracteres N, S, L, e O representando Norte, Sul, Leste e Oeste respectivamente.
        /// Estes catacteres podem estar presentes aleatóriamente na string de entrada.
        /// Uma string de entrada "NNNLLL" irá resultar em uma posição final "(3, 3)", assim como uma string "NLNLNL" irá resultar em "(3, 3)".
        /// 
        /// Caso o caracter X esteja presente, o mesmo irá cancelar a operação anterior. 
        /// Caso houver mais de um caracter X consecutivo, o mesmo cancelará mais de uma ação na quantidade em que o X estiver presente.
        /// Uma string de entrada "NNNXLLLXX" irá resultar em uma posição final "(1, 2)" pois a string poderia ser simplificada para "NNL".
        /// 
        /// Além disso, um número pode estar presente após o caracter da operação, representando o "passo" que a operação deve acumular.
        /// Este número deve estar compreendido entre 1 e 2147483647.
        /// Deve-se observar que a operação 'X' não suporta opção de "passo" e deve ser considerado inválido. Uma string de entrada "NNX2" deve ser considerada inválida.
        /// Uma string de entrada "N123LSX" irá resultar em uma posição final "(1, 123)" pois a string pode ser simplificada para "N123L"
        /// Uma string de entrada "NLS3X" irá resultar em uma posição final "(1, 1)" pois a string pode ser siplificada para "NL".
        /// 
        /// Caso a string de entrada seja inválida ou tenha algum outro problema, o resultado deve ser "(999, 999)".
        /// 
        /// OBSERVAÇÕES:
        /// Realizar uma implementação com padrões de código para ambiente de "produção". 
        /// Comentar o código explicando o que for relevânte para a solução do problema.
        /// Adicionar testes unitários para alcançar uma cobertura de testes relevânte.
        /// </summary>
        /// <param name="input">String no padrão "N1N2S3S4L5L6O7O8X"</param>
        /// <returns>String representando o ponto cartesiano após a execução dos comandos (X, Y)</returns>
        public static string Evaluate(string input)
        {
            float x = 0;
            float y = 0;

            // Verifico aqui, se é nulo o vazio, se é somente numero e se inicia com numero
            if (string.IsNullOrWhiteSpace(input) || input.All(char.IsDigit) || IniciaComNumero(input))
                return "(999, 999)";

            try
            {
                var inputSemSX = input.Replace("SX", string.Empty);

                var array = MontaArrayFinal(inputSemSX);

                var norte = array.Where(n => n.Equals("N")).Count();
                var sul = array.Where(s => s.Equals("S")).Count();
                var leste = array.Where(l => l.Equals("L")).Count();
                var oeste = array.Where(o => o.Equals("O")).Count();

                x = leste - oeste;
                y = norte - sul;

                return string.Format("({0}, {1})", x.ToString(), y.ToString());
            }
            catch
            {
                return "(999, 999)";
            }            
        }

        private static string[] MontaArrayFinal(string input)
        {
            var lista = new List<string>();
            var lastChar = '\0';
            var quantidadePasso = string.Empty;

            var inputLenght = input.ToCharArray().Count();

            for (int i = 0; i < inputLenght; i++)
            {
                // Faz validação se contem apenas operacoes validas
                Validacoes(input, lastChar, i);

                //operação atual é "passo"
                if (char.IsDigit(input[i]))
                {
                    //Vou concatenando na variavel os numeros que tem a quantidade de "passo"
                    quantidadePasso = string.Concat(quantidadePasso, input[i]);

                    //Vejo se existe o proximo indice no array
                    if (input.Length > i + 1)
                    {
                        //Se sim, verifico se é numero, se não for faço a multiplicação da operação
                        if (char.IsDigit(input[i + 1]))
                        {
                            continue;
                        }
                    }

                    MultiplicaOperacao(lastChar, quantidadePasso, lista);
                    quantidadePasso = string.Empty;
                    continue;
                }

                if (input[i].Equals('X'))
                    lista.RemoveAt(lista.Count() - 1);
                else
                    lista.Add(input[i].ToString());

                lastChar = input[i];
            }

            return lista.ToArray();
        }

        private static void MultiplicaOperacao(char lastChar, string value, List<string> lista)
        {
            var parse = Int32.TryParse(value.ToString(), out Int32 result);

            if (parse)
            {
                // removo o caracter inserido por ultimo.
                lista.RemoveAt(lista.Count() - 1);

                // reinsiro o caracter com a quantidade de operações passadas
                for (int i = 0; i < result; i++)
                {
                    lista.Add(lastChar.ToString());
                }
            }
            else
            {
                throw new Exception(@"Overflow");
            }
        }

        private static void Validacoes(string input, char lastChar, int i)
        {
            if (lastChar.Equals('X') && char.IsDigit(input[i]))
                throw new Exception(@"operação 'X' não suporta opção de 'passo'");
            else if (!char.IsDigit(input[i]) && !ComandoValido(input[i]))
                throw new Exception(@"contém operações inválidas");
        }

        #region Validações

        private static List<char> _comandos = new List<char>(new char[] { 'N', 'S', 'L', 'O', 'X'});
        private static bool ComandoValido(char value)
        {
            return _comandos.Contains(value);
        }

        public static bool IniciaComNumero(string input)
        {
            return ValidaRegex(@"^\d", input);
        }

        //Criei essa validação por regex, pois pode ser reaproveitado para outras validações
        private static bool ValidaRegex(string regex, string input)
        {
            var r = new Regex(regex);
            return r.Match(input).Success;
        }

        #endregion
    }
}
