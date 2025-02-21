using Data;
using Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public class DetalleFacturaService
    {
        private readonly Context _context;

        public DetalleFacturaService(Context context)
        {
            _context = context;
        }

        public async Task<List<DetalleFactura>> GetDetalleFacturas()
        {
            return await _context.DetalleFacturas.ToListAsync();
        }

        public DetalleFactura GetDetalleFactura(int id)
        {
            return _context.DetalleFacturas.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> CreateDetalleFactura(DetalleFactura detalleFactura)
        {
            _context.DetalleFacturas.Add(detalleFactura);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateDetalleFactura(DetalleFactura detalleFactura)
        {
            _context.DetalleFacturas.Update(detalleFactura);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDetalleFactura(int id)
        {
            var detalleFactura = _context.DetalleFacturas.FirstOrDefault(x => x.Id == id);
            _context.DetalleFacturas.Remove(detalleFactura);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
