using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiVersioningSwagger
{
    public class DefaultValuesFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;
            
            var maps = context.MethodInfo.GetCustomAttributes(true).OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToList();
            var version = maps[0].MajorVersion;
            

            operation.Deprecated = apiDescription.IsDeprecated();

            if (operation.Parameters == null) { return; }

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (parameter.Name == Settings.Header)
                {
                    parameter.Schema.Default = new OpenApiString(version.ToString());
                }
                
                parameter.Required = parameter.Required | description.IsRequired;
            }
        }
    }
}