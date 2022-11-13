using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApplication1;

#region anotherApproach

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.ReportApiVersions = true;
    config.AssumeDefaultVersionWhenUnspecified = true;


    //config.ApiVersionReader = new QueryStringApiVersionReader("api-version");
    //SwaggerConfig.UseQueryStringApiVersion("api-version");


    config.ApiVersionReader = new HeaderApiVersionReader("X-Version");
    SwaggerConfig.UseCustomHeaderApiVersion("X-Version");

    //config.ApiVersionReader = new MediaTypeApiVersionReader("v");
    //SwaggerConfig.UseAcceptHeaderApiVersion("v");

});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Api version  1", Version = "v1", Description = "Test Description", });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Api version  2", Version = "v2", Description = "Test Description", });
   /// options.OperationFilter<AddAcceptHeaderParameter>();
    options.OperationFilter<SwaggerParameterFilters>();
    options.DocumentFilter<SwaggerVersionMapping>();
    
    options.DocInclusionPredicate((version, desc) =>
    {
        if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
        var versions = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions);
        var maps = methodInfo.GetCustomAttributes(true).OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToArray();
        version = version.Replace("v", "");
        return versions.Any(v => v.ToString() == version && maps.Any(v => v.ToString() == version));
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API - V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API - V2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion


//
// #region minimalApproach
//
// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// builder.Services.AddApiVersioning(opt =>
// {
//     opt.DefaultApiVersion = new ApiVersion(1, 0);
//     opt.AssumeDefaultVersionWhenUnspecified = true;
//     opt.ReportApiVersions = true;
//     opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
//         new HeaderApiVersionReader("x-api-version"),
//         new MediaTypeApiVersionReader("x-api-version"));
// });
// // Add ApiExplorer to discover versions
// builder.Services.AddVersionedApiExplorer(setup =>
// {
//     setup.GroupNameFormat = "'v'VVV";
//     setup.SubstituteApiVersionInUrl = true;
// });
//
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
//
// var app = builder.Build();
//
// var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(options =>
//     {
//         foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
//         {
//             options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
//                 description.GroupName.ToUpperInvariant());
//         }
//     });
// }
//
// app.UseStaticFiles();
//
// app.UseHttpsRedirection();
//
// app.UseAuthorization();
//
// app.MapControllers();
//
// app.Run();
//
// #endregion
//
