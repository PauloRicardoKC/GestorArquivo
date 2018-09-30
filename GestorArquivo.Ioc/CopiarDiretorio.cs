using GestorArquivo.Servico;
using System;
using System.IO;

namespace GestorArquivo.Ioc
{
    public class CopiarDiretorio
    {
        GravarArquivo _gravaArquivo = new GravarArquivo();
        LogWindows _log = new LogWindows();

        public void Copiar(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Obter os subdiretórios do diretório específico.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                _log.CriarLog("O diretório de origem não existe ou não pôde ser encontrado: "
                    + sourceDirName);
                throw new DirectoryNotFoundException(
                    "O diretório de origem não existe ou não pôde ser encontrado: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // Se o diretório destino não existir, crie-o.
            if (!Directory.Exists(destDirName))
            {
                string name = Directory.CreateDirectory(destDirName).Name;
                _gravaArquivo.IncluirArquivoCopiado(name, sourceDirName, true, false);
            }

            // Obtenha os arquivos no diretório e copie-os para o novo local.
            // Valide o arquivo existente e continue
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                try
                {
                    string sourcepath = Path.Combine(sourceDirName, file.Name);
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, false);
                    _gravaArquivo.IncluirArquivoCopiado(file.Name, sourcepath, true, false);

                }
                catch (Exception error)
                {
                    _log.CriarLog("O arquivo " + file.Name + " localizado em " + sourceDirName + " não foi copiado. Mensagem de erro: " + error.Message);
                    continue;
                }
            }

            // Se existir subdiretórios, copie-os seus conteúdos para o novo local.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string sourcepath = Path.Combine(sourceDirName, subdir.Name);
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    Copiar(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
