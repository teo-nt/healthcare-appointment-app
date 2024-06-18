using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HealthcareAppointmentApp.Helpers
{
    public class TimeOnlySchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(TimeOnly))
            {
                schema.Type = "string";
                schema.Format = "time";
                schema.Example = new OpenApiString("12:30:00");
            }
        }
    }
}
