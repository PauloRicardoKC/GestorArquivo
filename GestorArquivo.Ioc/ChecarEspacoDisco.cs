using System;
using System.IO;

namespace GestorArquivo.Ioc
{
    public class ChecarEspacoDisco
    {
        public decimal PercentualDeUsoDisco(string drive)
        {
            DriveInfo d = new DriveInfo(drive);

            decimal tamanho = Decimal.Round(d.TotalSize, 2);
            decimal livre = Decimal.Round(d.TotalFreeSpace, 2);
            decimal usado = Decimal.Round(tamanho - livre, 2);
            decimal percentualUsado = Decimal.Round(usado / tamanho, 2) * 100;

            return percentualUsado;
        }
    }
}
