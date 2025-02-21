using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entidades
{
    public class DetalleFactura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Factura Facturas { get; set; }
        public int FacturaId { get; set; }

        [Required]
        [StringLength(50)] // Longitud máxima de 50 caracteres
        [Column(TypeName = "varchar(50)")]
        public string Producto { get; set; }

        [Required]
        public double Cantidad { get; set; }

        [Required]
        public double PrecioUnitario { get; set; }

        [Required]
        public double Subtotal { get; set; }

    }
}
