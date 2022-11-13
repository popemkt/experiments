using System.Reflection;
using ApiVersioningSwagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

#region anotherApproach

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddApiVersioning(config =>
// {
//     config.DefaultApiVersion = new ApiVersion(1, 0);
//     config.ReportApiVersions = true;
//     config.AssumeDefaultVersionWhenUnspecified = true;
//
//     //Query string
//     //config.ApiVersionReader = new QueryStringApiVersionReader("api-version");
//     //SwaggerConfig.UseQueryStringApiVersion("api-version");
//
//     config.ApiVersionReader = new HeaderApiVersionReader("X-Version");
//     SwaggerConfig.UseCustomHeaderApiVersion("X-Version");
//
//     //AcceptHeader
//     //config.ApiVersionReader = new MediaTypeApiVersionReader("v");
//     //SwaggerConfig.UseAcceptHeaderApiVersion("v");
//
// });
//
// builder.Services.AddSwaggerGen(options =>
// {
//     options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Api version  1", Version = "v1", Description = "Test Description", });
//     options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Api version  2", Version = "v2", Description = "Test Description", });
//    /// options.OperationFilter<AddAcceptHeaderParameter>();
//     options.OperationFilter<SwaggerParameterFilters>();
//     options.DocumentFilter<SwaggerVersionMapping>();
//     
//     options.DocInclusionPredicate((version, desc) =>
//     {
//         if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
//         var versions = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions);
//         var maps = methodInfo.GetCustomAttributes(true).OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToArray();
//         version = version.Replace("v", "");
//         return versions.Any(v => v.ToString() == version && maps.Any(v => v.ToString() == version));
//     });
// });

#endregion



#region minimalApproach

// Add services to the container.

builder.Services.AddSwaggerGen(opt => opt.OperationFilter<DefaultValuesFilter>());

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    // opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
    //     new HeaderApiVersionReader(Settings.Header),
    //     new MediaTypeApiVersionReader(Settings.Header));
    opt.ApiVersionReader = new HeaderApiVersionReader(Settings.Header);
});
// Add ApiExplorer to discover versions

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.OperationFilter<DefaultValuesFilter>());
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

#endregion

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });

}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class Settings
{
    public static string Header = "x-version";
}