using Microsoft.AspNetCore.Mvc;
using Model.Responses;
using Model.Services;

namespace API.Controllers
{
    [Route("api/factura")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;
        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet]
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

        [HttpGet("{id}")]
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

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
