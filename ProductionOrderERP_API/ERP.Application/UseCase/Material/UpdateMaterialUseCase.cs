﻿using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class UpdateMaterialUseCase
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public UpdateMaterialUseCase(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<Material?> Execute(int materialId, UpdateMaterialRequest materialDto)
        {
            var materialEntity = await _materialRepository.GetMaterialAsync(materialId);
            _mapper.Map(materialDto, materialEntity);
            return await _materialRepository.UpdateMaterialAsync(materialEntity);
        }
    }
}
