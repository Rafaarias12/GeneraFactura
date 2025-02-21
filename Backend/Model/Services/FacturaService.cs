using Data;
using Data.Entidades;
using Microsoft.EntityFrameworkCore;
using Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly Context _context;
        public FacturaService(Context context)
        {
            _context = context;
        }

        public async Task<List<Factura>> GetFacturas()
        {
            return await _context.Facturas.ToListAsync();
        }
        public FacturaResponse GetFactura(int id)
        {

            var factura = _context.Facturas
            .Include(f => f.DetalleFacturas)
            .FirstOrDefault(f => f.Id == id);

            if (factura == null)
            {
                return null;
            }

            // Mapear la entidad Factura a FacturaDto
            var facturaDto = new FacturaResponse
            {
                Id = factura.Id,
                Cliente = factura.Cliente,
                Fecha = factura.Fecha,
                Total = factura.Total,
                DetalleFacturas = factura.DetalleFacturas.Select(d => new DetalleFacturaResponse
                {
                    Id = d.Id,
                    Producto = d.Producto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            };

            return facturaDto;

        }
        public async Task<bool> CreateFactura(FacturaResponse model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Factura factura = new Factura
                {
                    Fecha = DateTime.SpecifyKind(model.Fecha, DateTimeKind.Utc),
                    Cliente = model.Cliente,
                    Total = model.Total == 0 && model.DetalleFacturas.Any()
                        ? (int)model.DetalleFacturas.Sum(d => d.Subtotal)
                        : model.Total
                };

                _context.Facturas.Add(factura);
                await _context.SaveChangesAsync();

                var facturaId = factura.Id;

                var detalles = model.DetalleFacturas.Select(d => new DetalleFactura
                {
                    FacturaId = facturaId, // Asegurar que el ID es correcto
                    Producto = d.Producto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList();

                _context.DetalleFacturas.AddRange(detalles);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync(); // Confirmar la transacción
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Revertir si hay error
                Console.WriteLine($"Error: {ex.InnerException?.Message ?? ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateFactura(FacturaResponse model, int id)
        {
            var facturaExistente = await _context.Facturas
            .Include(f => f.DetalleFacturas) // Incluir los detalles de la factura
            .FirstOrDefaultAsync(f => f.Id == id);

            if (facturaExistente == null)
            {
                return false;
            }

            facturaExistente.Cliente = model.Cliente;
            facturaExistente.Fecha = model.Fecha;
            facturaExistente.Total = model.Total;

            foreach (var detalle in model.DetalleFacturas)
            {
                var detalleExistente = facturaExistente.DetalleFacturas.FirstOrDefault(d => d.Id == detalle.Id);
                if (detalleExistente == null)
                {
                    facturaExistente.DetalleFacturas.Add(new DetalleFactura
                    {
                        Producto = detalle.Producto,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = detalle.PrecioUnitario,
                        Subtotal = detalle.Subtotal
                    });
                }
                else
                {
                    detalleExistente.Producto = detalle.Producto;
                    detalleExistente.Cantidad = detalle.Cantidad;
                    detalleExistente.PrecioUnitario = detalle.PrecioUnitario;
                    detalleExistente.Subtotal = detalle.Subtotal;
                }
            }

            var detallesAEliminar = facturaExistente.DetalleFacturas
            .Where(d => !model.DetalleFacturas.Any(dr => dr.Id == d.Id))
            .ToList();

            foreach (var detalle in detallesAEliminar)
            {
                _context.DetalleFacturas.Remove(detalle);
            }

            // Calcular el total de la factura (si no se proporciona)
            if (model.Total == 0)
            {
                facturaExistente.Total = (int)facturaExistente.DetalleFacturas.Sum(d => d.Subtotal);
            }

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Mapear la entidad Factura a FacturaResponse para la respuesta
            var response = new FacturaResponse
            {
                Id = facturaExistente.Id,
                Cliente = facturaExistente.Cliente,
                Fecha = facturaExistente.Fecha,
                Total = facturaExistente.Total,
                DetalleFacturas = facturaExistente.DetalleFacturas.Select(d => new DetalleFacturaResponse
                {
                    Id = d.Id,
                    Producto = d.Producto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            };
            return true;
        }

        public async Task<bool> DeleteFactura(int id)
        {
            var factura = _context.Facturas
            .Include(f => f.DetalleFacturas) // Incluir los detalles de la factura
            .FirstOrDefault(x => x.Id == id);

            if (factura == null)
            {
                return false; // La factura no existe
            }

            // Eliminar los detalles de la factura
            _context.DetalleFacturas.RemoveRange(factura.DetalleFacturas);

            // Eliminar la factura
            _context.Facturas.Remove(factura);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
