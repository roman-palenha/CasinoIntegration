﻿using System.Text.Json;

namespace CasinoIntegration.BusinessLayer.DTO.Response
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
