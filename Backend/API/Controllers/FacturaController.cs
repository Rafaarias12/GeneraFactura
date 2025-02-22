using Microsoft.AspNetCore.Mvc;
using Model.Responses;
using Model.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    /// <summary>
    /// Controlador para facturas.
    /// </summary>
    /// 
    [Route("api/factura")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;
        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        /// <summary>
        /// Obtiene todas las facturas.
        /// </summary>
        /// <returns>Una lista de facturas.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las facturas", Description = "Retorna una lista de todas las facturas disponibles.")]
        public async Task<ActionResult<List<FacturaResponse>>> GetFacturas()
        {
            try
            {
                var facturas = await _facturaService.GetFacturas();
                return Ok(facturas);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna factura especifica
        /// </summary>
        /// <returns>Retorna factura especifica</returns>
        /// <response code="200">Retorna factura especifica</response>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna factura especifica", Description = "Retorna factura especifica según su id")]
        public async Task<ActionResult<FacturaResponse>> GetFactura(int id)
        {
            try
            {
                return Ok(_facturaService.GetFactura(id));
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Crea factura
        /// </summary>
        /// <returns>Crea factura</returns>
        /// <response code="200">Crea factura</response>
        [HttpPost]
        [SwaggerOperation(Summary = "Agrega factura", Description = "Agrega nueva factura")]
        public async Task<ActionResult<FacturaResponse>> CreateFactura(FacturaResponse model)
        {
            try
            {
                await _facturaService.CreateFactura(model);
                return Ok();
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Edita factura
        /// </summary>
        /// <returns>Edita factura</returns>
        /// <response code="200">Edita factura</response>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Edita Factura", Description = "Edita factura según su id")]
        public async Task<ActionResult<FacturaResponse>> UpdateFactura(FacturaResponse model, int id)
        {
            try
            {
                await _facturaService.UpdateFactura(model, id);
                return Ok();
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina factura
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Elimina factura</response>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina factura especifica", Description = "Elimina factura especifica según su id")]
        public async Task<ActionResult<FacturaResponse>> DeleteFactura(int id)
        {
            try
            {
                await _facturaService.DeleteFactura(id);
                return Ok();
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
