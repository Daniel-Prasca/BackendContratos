using BackendContratos.Dtos;
using BackendContratos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BackendContratos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedoresService _service;

        public ProveedoresController(IProveedoresService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var proveedores = await _service.GetAllAsync();
            return Ok(proveedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var proveedor = await _service.GetByIdAsync(id);
            if (proveedor == null) return NotFound();
            return Ok(proveedor);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] ProveedorCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] ProveedorUpdateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return Ok(new {message = "Proveedor actulizado correctamente"});
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok(new {mensagge = "Proveedor eliminado correctamente"});
        }
    }
}
