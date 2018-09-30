using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GestorArquivo.Data.Contexto
{
    public class ContextoGestorArquivo : DbContext
    {
        static ContextoGestorArquivo()
        {
            Database.SetInitializer<ContextoGestorArquivo>(null);
        }

        #region Construtor
        public ContextoGestorArquivo()
            : base("name=ContextoGestorArquivo")
        {
            //Propriedade é usada para definir o comportamento de carregamento lento de objetos relacionados.
            Configuration.LazyLoadingEnabled = false;

            //Algoritmo de detecção de mudanças.
            Configuration.AutoDetectChangesEnabled = true;

            Configuration.ProxyCreationEnabled = false;
        }

        #endregion Construtor

        #region Override
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Não setar nomes da tabela no plural.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Não deletar em cascata quando for um para vários.
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Não deletar em cascata quando for vários para vários.
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(typeof(ContextoGestorArquivo).Assembly);
        }

        #endregion Override
    }
}
