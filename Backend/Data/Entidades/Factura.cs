using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entidades
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Cliente { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int Total { get; set; }

        public ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}
