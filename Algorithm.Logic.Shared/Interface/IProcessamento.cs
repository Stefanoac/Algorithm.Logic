using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm.Logic.Shared.Interface
{
    public interface IProcessamento
    {
        string Processar(string input);
        bool ValidaCoordenadas(string input);
    }
}
