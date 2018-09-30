using System;
using System.ComponentModel.DataAnnotations;

namespace GestorArquivo.Dominio.Entidades
{
    public class Arquivo
    {
        [Key]
        [Display(Name = "Código")]
        public int ArquivoId { get; set; }

        [Display(Name = "Nome do arquivo")]
        public string NomeArquivo { get; set; }

        [Display(Name = "Caminho do arquivo")]
        public string CaminhoArquivo { get; set; }

        [Display(Name = "Arquivo copiado?")]
        public bool ArquivoCopiado { get; set; }

        [Display(Name = "Arquivo deletado?")]
        public bool ArquivoDeletado { get; set; }

        [Display(Name = "Data da cópia")]
        public DateTime? DataCopiado { get; set; }

        [Display(Name = "Data de exclusão")]
        public DateTime? DataDeletado { get; set; }
    }
}
