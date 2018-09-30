namespace GestorArquivo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GestorArquivoDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arquivos",
                c => new
                    {
                        ArquivoId = c.Int(nullable: false, identity: true),
                        NomeArquivo = c.String(),
                        CaminhoArquivo = c.String(),
                        ArquivoCopiado = c.Boolean(nullable: false),
                        ArquivoDeletado = c.Boolean(nullable: false),
                        DataCopiado = c.DateTime(),
                        DataDeletado = c.DateTime(),
                    })
                .PrimaryKey(t => t.ArquivoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Arquivos");
        }
    }
}
