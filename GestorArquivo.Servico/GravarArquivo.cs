using GestorArquivo.Data.Repositorio;
using GestorArquivo.Dominio.Entidades;
using System;

namespace GestorArquivo.Servico
{
    public class GravarArquivo
    {
        UnitOfWork<Arquivo> _unitOfWork = new UnitOfWork<Arquivo>();

        public void IncluirArquivoCopiado(string nome, string caminho, bool copiado, bool deletado)
        {
            Arquivo arquivo = new Arquivo
            {
                NomeArquivo = nome,
                CaminhoArquivo = caminho,
                ArquivoCopiado = copiado,
                ArquivoDeletado = deletado,
                DataCopiado = DateTime.Now,
            };

            _unitOfWork.BaseRepositorio.Incluir(arquivo);
        }

        public void AlterarArquivoDeletado(string nome, string caminho, bool copiado, bool deletado)
        {
            var arquivo = _unitOfWork.BaseRepositorio.PegarPrimeiro(x => x.NomeArquivo == nome && x.CaminhoArquivo == caminho);

            arquivo.ArquivoDeletado = deletado;
            arquivo.DataDeletado = DateTime.Now;

            _unitOfWork.BaseRepositorio.Alterar(arquivo);
        }
    }
}
