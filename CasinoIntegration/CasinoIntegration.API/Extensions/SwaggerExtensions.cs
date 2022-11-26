using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace CasinoIntegration.API.Extensions
{
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Extension to add xml documentation in swagger
        /// </summary>
        /// <param name="o">Swagger options</param>
        public static void AddSwaggerDocumentation(this SwaggerGenOptions o)
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        }
    }
}
