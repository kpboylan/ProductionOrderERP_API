using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Application.UseCase;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Core.Service;
using ProductionOrderERP_API.ERP.Core.Helper;
using Serilog;

namespace ProductionOrderERP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly CreateMaterialUseCase _createMaterialUseCase;
        private readonly GetMaterialsUseCase _getMaterialsUseCase;
        private readonly GetActiveMaterialsUseCase _getActiveMaterialsUseCase;
        private readonly GetMaterialUseCase _getMaterialUseCase;
        private readonly GetUOMsUseCase _getUOMsUseCase;
        private readonly UpdateMaterialUseCase _updateMaterialUseCase;
        private readonly GetMaterialTypesUseCase _getMaterialTypesUseCase;
        private readonly PublishCreateMaterialUseCase _publishMaterialUseCase;
        private readonly PublishUpdateMaterialUseCase _publishUpdateMaterialUseCase;

        public MaterialController(
            CreateMaterialUseCase createMaterialUseCase,
            GetUOMsUseCase getUOMsUseCase,
            GetMaterialsUseCase getMaterialsUseCase,
            GetActiveMaterialsUseCase getActiveMaterialsUseCase,
            GetMaterialUseCase getMaterialUseCase,
            UpdateMaterialUseCase updateMaterialUseCase,
            GetMaterialTypesUseCase getMaterialTypesUseCase,
            PublishCreateMaterialUseCase publishMaterialUseCase,
            PublishUpdateMaterialUseCase publishUpdateMaterialUseCase)
        {
            _createMaterialUseCase = createMaterialUseCase;
            _getUOMsUseCase = getUOMsUseCase;
            _getMaterialsUseCase = getMaterialsUseCase;
            _getActiveMaterialsUseCase = getActiveMaterialsUseCase;
            _getMaterialUseCase = getMaterialUseCase;
            _updateMaterialUseCase = updateMaterialUseCase;
            _getMaterialTypesUseCase = getMaterialTypesUseCase;
            _publishMaterialUseCase = publishMaterialUseCase;
            _publishUpdateMaterialUseCase = publishUpdateMaterialUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaterial([FromBody] CreateMaterialRequest material)
        {
            if (material == null)
            {
                return BadRequest("Material data is required.");
            }

            try
            {
                var response = await _createMaterialUseCase.Execute(material);
                return Ok(response);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost("rabbitMqCreate")]
        
        public async Task<IActionResult> CreateRabbitMqMaterial([FromBody] PublishMaterialRequest material)
        {
            if (material == null)
            {
                return BadRequest("Material data is required.");
            }

            try
            {
                var response = await _publishMaterialUseCase.Execute(material);

                return Ok(response);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetMaterials()
        {
            try
            {
                var materialDtos = await _getMaterialsUseCase.Execute();

                return Ok(materialDtos);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveMaterials()
        {
            try
            {
                var materialDtos = await _getActiveMaterialsUseCase.Execute();

                return Ok(materialDtos);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getMaterial")]
        public async Task<IActionResult> GetMaterial(int materialId)
        {
            try
            {
                var materialDto = await _getMaterialUseCase.Execute(materialId);

                return Ok(materialDto);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("rabbitMqUpdate")]
        public async Task<IActionResult> UpdateRabbitMqMaterial([FromBody] PublishMaterialRequest material, int materialId)
        {
            if (material == null)
            {
                return BadRequest("Material data is null");
            }

            try
            {
                await _publishUpdateMaterialUseCase.Execute(material, materialId);

                return NoContent();
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{materialId}")]
        public async Task<IActionResult> UpdateMaterial([FromBody] UpdateMaterialRequest material, int materialId)
        {
            if (material == null)
            {
                return BadRequest("Material data is null");
            }

            try
            {
                await _updateMaterialUseCase.Execute(materialId, material);

                return NoContent();
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("materialTypes")]
        public async Task<IActionResult> GetMaterialTypes()
        {
            try
            {
                var materialTypeDtos = await _getMaterialTypesUseCase.Execute();

                return Ok(materialTypeDtos);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("UOM")]
        public async Task<IActionResult> GetUOM()
        {
            try
            {
                var uomDtos = await _getUOMsUseCase.Execute();

                return Ok(uomDtos);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
