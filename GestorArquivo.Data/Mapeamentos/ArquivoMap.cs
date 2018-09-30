using GestorArquivo.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace GestorArquivo.Data.Mapeamentos
{
    public class ArquivoMap : EntityTypeConfiguration<Arquivo>
    {
        public ArquivoMap()
        {
            ToTable("Arquivos", "dbo");

            HasKey(x => x.ArquivoId);

            Property(x => x.ArquivoId);
            Property(x => x.NomeArquivo);
            Property(x => x.CaminhoArquivo);
            Property(x => x.ArquivoCopiado);
            Property(x => x.ArquivoDeletado);
            Property(x => x.DataCopiado);
            Property(x => x.DataDeletado);
        }
    }
}
