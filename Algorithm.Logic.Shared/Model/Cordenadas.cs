using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm.Logic.Shared.Model
{
    public class Coordenadas
    {
        public Coordenadas()
        {
            this.X = 0;
            this.Y = 0;
            this.Input = string.Empty;
            CordenadasInvalidas = string.Format(@"({0}, {1})", 999, 999);
        }


        public int X { get; private set; }
        public int Y { get; private set; }
        public string Input { get; set; }
        public string CordenadasInvalidas { get; private set; }

        public void AlteraCoordenada(string passo, int valor)
        {
            switch (passo)
            {
                case "N":
                    this.Y += valor;
                    break;

                case "S":
                    this.Y -= valor;
                    break;

                case "L":
                    this.X += valor;
                    break;

                case "O":
                    this.X -= valor;
                    break;
            }
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
