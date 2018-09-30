using GestorArquivo.Servico;
using System;
using System.IO;
using System.Linq;

namespace GestorArquivo.Ioc
{
    public class DeletarDiretorio
    {
        GravarArquivo _gravaArquivo = new GravarArquivo();
        ValidaArquivo _validaArquivo = new ValidaArquivo();
        ChecarDataCriacaoArquivo _dataCriacao = new ChecarDataCriacaoArquivo();
        LogWindows _log = new LogWindows();

        public void Deletar(string sourceDirName, string destDirName, bool copySubDirs)
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

            // Obter os arquivos no diretório origem para exclusão.
            // Valida se o arquivo foi copiado 
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                try
                {
                    if (_dataCriacao.ArquivoMaiorQueDoisMeses(file))
                    {
                        string sourcepath = Path.Combine(sourceDirName, file.Name);
                        string temppath = Path.Combine(destDirName, file.Name);

                        var permiteExcluirArquivo = _validaArquivo.VerificaArquivoCopiadoParaExclusao(file.Name, sourcepath);

                        if (permiteExcluirArquivo)
                        {
                            file.Delete();
                            _gravaArquivo.AlterarArquivoDeletado(file.Name, sourcepath, true, true);
                        }
                    }
                }
                catch (Exception error)
                {
                    _log.CriarLog("O arquivo " + file.Name + " localizado em " + sourceDirName + " não foi excluído. Mensagem de erro: " + error.Message);
                    continue;
                }
            }

            // Busca subdiretórios origem para exclusão.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string sourcepath = Path.Combine(sourceDirName, subdir.Name);
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    Deletar(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public void DeletarDiretorios(string sourceDirName, string destDirName, bool copySubDirs, string parent = null, string parentFullPath = null)
        {
            try
            {
                // Obter os subdiretórios para o diretório específico.
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);

                if (!dir.Exists)
                {
                    _log.CriarLog("O diretório de origem não existe ou não pôde ser encontrado: "
                        + sourceDirName);
                    throw new DirectoryNotFoundException(
                        "O diretório de origem não existe ou não pôde ser encontrado: "
                        + sourceDirName);
                }

                // Obter os diretórios origem para exclusão.           
                DirectoryInfo[] dirs = dir.GetDirectories();

                if (!dirs.Any() && sourceDirName != "C:\\ArquivosOrigem")
                {
                    FileInfo[] files = dir.GetFiles();

                    if (!files.Any())
                    {
                        Directory.Delete(sourceDirName, true);
                        _gravaArquivo.AlterarArquivoDeletado(dir.Name, sourceDirName, true, true);
                    }
                }

                foreach (DirectoryInfo subdir in dirs)
                {
                    try
                    {
                        string subParent = subdir.Parent.ToString(); //nome diretório pai
                        string subParentFullPath = subdir.Parent.FullName.ToString(); //caminho diretório pai
                        string sourcepath = Path.Combine(sourceDirName, subdir.Name);

                        DeletarDiretorios(subdir.FullName, sourcepath, copySubDirs, subParent, subParentFullPath);

                    }
                    catch (Exception error)
                    {
                        _log.CriarLog("A pasta " + subdir.FullName + " localizado em " + sourceDirName + " não foi excluído. Mensagem de erro: " + error.Message);
                        continue;
                    }
                }
            }
            catch (Exception error)
            {
                _log.CriarLog("Mensagem de erro: " + error.Message);
            }
        }
    }
}
