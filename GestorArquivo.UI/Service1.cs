using GestorArquivo.Ioc;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;

namespace GestorArquivo.UI
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Start();
        }

        public void Start()
        {
            ChecarEspacoDisco disco = new ChecarEspacoDisco();

            var caminhoOrigem = ConfigurationManager.AppSettings["CaminhoOrigem"];
            var caminhoDestino = ConfigurationManager.AppSettings["CaminhoDestino"];
            var driveDisco = ConfigurationManager.AppSettings["Disco"];

            CopiarDiretorio copiar = new CopiarDiretorio();
            copiar.Copiar(caminhoOrigem, caminhoDestino, true);

            if (disco.PercentualDeUsoDisco(driveDisco) >= 90)
            {
                DeletarDiretorio deletar = new DeletarDiretorio();
                deletar.Deletar(caminhoOrigem, caminhoDestino, true);
                deletar.DeletarDiretorios(caminhoOrigem, caminhoDestino, true, null, null);
            }

            OnStop();
        }

        protected override void OnStop()
        {
            Process[] processes = Process.GetProcessesByName("GestorArquivo.UI");

            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
    }
}
