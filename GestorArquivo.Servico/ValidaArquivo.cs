using GestorArquivo.Data.Repositorio;
using GestorArquivo.Dominio.Entidades;

namespace GestorArquivo.Servico
{
    public class ValidaArquivo
    {
        UnitOfWork<Arquivo> _unitOfWork = new UnitOfWork<Arquivo>();

        public bool VerificaArquivoCopiadoParaExclusao(string nome, string caminho)
        {
            var arquivo = _unitOfWork.BaseRepositorio.PegarPrimeiro(x => x.NomeArquivo == nome && x.CaminhoArquivo == caminho);

            if (arquivo == null)
                return false;

            if (arquivo.ArquivoCopiado == true)
                return true;

            return false;
        }
    }
}
