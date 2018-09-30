using System;
using System.IO;

namespace GestorArquivo.Ioc
{
    public class ChecarDataCriacaoArquivo
    {
        public bool ArquivoMaiorQueDoisMeses(FileInfo file)
        {
            var dataCriacao = file.LastWriteTime;
            var dataAtual = DateTime.Now;

            var diferencaMeses = dataAtual.Subtract(dataCriacao).Days / (365.25 / 12);

            if (diferencaMeses > 2)
                return true;

            return false;
        }
    }
}
