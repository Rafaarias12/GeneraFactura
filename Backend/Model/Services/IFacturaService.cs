using Data.Entidades;
using Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public interface IFacturaService
    {
        public Task<List<Factura>> GetFacturas();
        public FacturaResponse GetFactura(int id);
        public Task<bool> CreateFactura(FacturaResponse model);
        public Task<bool> UpdateFactura(FacturaResponse model, int id);
        public Task<bool> DeleteFactura(int id);

    }
}
