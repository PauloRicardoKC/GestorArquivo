using System.Diagnostics;

namespace GestorArquivo.Ioc
{
    public class LogWindows
    {
        public void CriarLog(string mensagem)
        {
            string sSource;
            string sLog;

            sSource = "Application";
            sLog = "Gestor de Arquivos";

            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, mensagem, EventLogEntryType.Warning, 234);
        }
    }
}
