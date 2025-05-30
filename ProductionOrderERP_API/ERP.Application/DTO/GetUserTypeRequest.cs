﻿
using System.ComponentModel.DataAnnotations;

namespace ProductionOrderERP_API.ERP.Application.DTO
{
    public class GetUserTypeRequest
    {
        public int UserTypeID { get; set; }

        public required string Type { get; set; }

        public bool Active { get; set; }
    }
}
