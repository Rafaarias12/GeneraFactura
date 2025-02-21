using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Responses
{
    public class FacturaResponse
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public int Total { get; set; }
        public List<DetalleFacturaResponse> DetalleFacturas { get; set; }
    }
}
