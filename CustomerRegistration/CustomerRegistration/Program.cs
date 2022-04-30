using AutoMapper;
using CustomerRegistration.Application.Application;
using CustomerRegistration.Application.Interface;
using CustomerRegistration.Core;
using CustomerRegistration.Core.ApiRequestModels;
using CustomerRegistration.Core.ApiResponceModels;
using CustomerRegistration.Core.Utilities;
using CustomerRegistration.Infrastructure;
using CustomerRegistration.Infrastructure.Interface;
using CustomerRegistration.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Net;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

var builder = WebApplication.CreateBuilder(args);


//Register inftr depedencys

builder.Services.AddTransient<ICustomerInfrastructure, CustomerService>();
builder.Services.AddTransient<IFileDetailsInfrastructure, FileDetailsServices>();


//Register application depedencys

builder.Services.AddTransient<ICustomer, Customer>();
builder.Services.AddTransient<IFileDetails, FileDetails>();



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:Dev"]));


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Customer", Version = "v1" });
    c.SwaggerGeneratorOptions.DescribeAllParametersInCamelCase = false;
});


var provider = builder.Services.BuildServiceProvider();
var context = provider.GetService<IHttpContextAccessor>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
      builder =>
      {
          builder.WithOrigins("http://localhost:4200", "http://localhost");
      });
});


builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperRegister());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

await using var app = builder.Build();


app.Use(async (context, next) =>
{
    //Access-Control-Allow-Origin
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
    context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin, Origin, X-Requested-With, Content-Type, Accept, Authorization, content-type, Bearer, token, observe, UserID");
    context.Response.Headers.Add("Expires", "-1");
    await next();
});
app.UseCors(MyAllowSpecificOrigins);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


#region customer api
//customer apis
app.MapPost("/api/addcustomer", [AllowAnonymous] async (ApiRequestCustomer apiRequestCustomer, ICustomer customer) =>
{
    SingleResponse<ApiRequestCustomer> response = new SingleResponse<ApiRequestCustomer>();
    try
    {
        var res = await Task.FromResult(customer.AddCustomer(apiRequestCustomer)).Result;
        response.Message = ResponseConstrains.RESULT_SUCCESS;
        response.Data = res;
        response.StatusCode = (int)HttpStatusCode.OK;

    }
    catch (System.Exception ex)
    {
        response.Data = null;
        response.Message = ResponseConstrains.RESULT_FAIL;
        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        throw;
    }
    return Results.Ok(response);
});

app.MapPut("/api/editcustomer", [AllowAnonymous] async (ApiRequestCustomer apiRequestCustomer, ICustomer customer) =>
{
    SingleResponse<ApiRequestCustomer> response = new SingleResponse<ApiRequestCustomer>();
    try
    {
        var res = await Task.FromResult(customer.UpdateCustomer(apiRequestCustomer)).Result;
        response.Message = ResponseConstrains.RESULT_SUCCESS;
        response.Data = res;
        response.StatusCode = (int)HttpStatusCode.OK;
    }
    catch (Exception ex)
    {
        response.Data = null;
        response.Message = ResponseConstrains.RESULT_FAIL;
        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        throw;
    }
    return Results.Ok(response);
});

app.MapDelete("/api/deletecustomer", [AllowAnonymous] (int id, ICustomer customer) =>
{
    SingleResponse<ApiRequestCustomer> response = new SingleResponse<ApiRequestCustomer>();

    try
    {
        customer.DeleteCustomer(id);
        response.Message = ResponseConstrains.RESULT_SUCCESS;
        response.Data = null;
        response.StatusCode = (int)HttpStatusCode.OK; ;
    }
    catch (Exception ex)
    {
        response.Data = null;
        response.Message = ResponseConstrains.RESULT_FAIL;
        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
    }

    return Results.Ok(response);
});

app.MapGet("/api/getcustomer", [AllowAnonymous] async (int id, ICustomer customer) =>
{
    SingleResponse<ApiRequestCustomer> response = new SingleResponse<ApiRequestCustomer>();

    try
    {
        var res = await customer.GetCustomer(id);
        response.Message = "Success";
        response.Data = res;
        response.StatusCode = 200;
    }
    catch (System.Exception ex)
    {
        response.Data = null;
        response.Message = ResponseConstrains.RESULT_FAIL;
        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        throw;
    }
    return Results.Ok(response);
});

app.MapGet("/api/getcustomerall", [AllowAnonymous] async (ICustomer customer) =>
{
    ListResponse<ApiRequestCustomer> response = new ListResponse<ApiRequestCustomer>();

    try
    {
        var res = await customer.GetCustomerAll();
        response.Message = ResponseConstrains.RESULT_SUCCESS;
        response.Data = res;
        response.StatusCode = (int)HttpStatusCode.OK;
    }
    catch (System.Exception ex)
    {
        response.Data = null;
        response.Message = ResponseConstrains.RESULT_FAIL;
        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        throw;
    }
    return Results.Ok(response);
});
#endregion


#region file api
//file apis
app.MapPost("/api/addfile", [AllowAnonymous] async (HttpRequest req, int CustomerId, IFileDetails fileDetails) =>
{
    if (!req.HasFormContentType)
    {
        return Results.BadRequest();
    }

    var form = await req.ReadFormAsync();
    var file = form.Files["file"];

    if (file is null)
    {
        return Results.BadRequest();
    }

    ApiRequestFileDetails apiRequestFileBlog = new ApiRequestFileDetails();
    apiRequestFileBlog.CustomerId = CustomerId;

    SingleResponse<ApiRequestFileDetails> response = new SingleResponse<ApiRequestFileDetails>();
    try
    {
        var res = await Task.FromResult(fileDetails.AddFile(file, apiRequestFileBlog)).Result;
        response.Message = ResponseConstrains.RESULT_SUCCESS;
        response.Data = res;
        response.StatusCode = (int)HttpStatusCode.OK;

    }
    catch (System.Exception ex)
    {
        response.Data = null;
        response.Message = ResponseConstrains.RESULT_FAIL;
        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        throw;
    }
    return Results.Ok(response);
}).Accepts<IFormFile>("multipart/form-data");

app.MapPut("/api/editfile", [AllowAnonymous] async (HttpRequest req, int id, int CustomerId, IFileDetails fileDetails) =>
{
    if (!req.HasFormContentType)
    {
        return Results.BadRequest();
    }

    var form = await req.ReadFormAsync();
    var file = form.Files["file"];

    if (file is null)
    {
        return Results.BadRequest();
    }
    ApiRequestFileDetails apiRequestFileDetails = new ApiRequestFileDetails();
    apiRequestFileDetails.Id = id;
    apiRequestFileDetails.CustomerId = CustomerId;


    SingleResponse<ApiRequestFileDetails> response = new SingleResponse<ApiRequestFileDetails>();
    try
    {
        var res = await Task.FromResult(fileDetails.UpdateFile(file, apiRequestFileDetails)).Result;
        response.Message = ResponseConstrains.RESULT_SUCCESS;
        response.Data = res;
        response.StatusCode = (int)HttpStatusCode.OK;
    }
    catch (Exception ex)
    {
        response.Data = null;
        response.Message = ResponseConstrains.RESULT_FAIL;
        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        throw;
    }
    return Results.Ok(response);
}).Accepts<IFormFile>("multipart/form-data");

app.MapDelete("/api/deletefile", [AllowAnonymous] (int id, IFileDetails fileDetails) =>
{
    SingleResponse<ApiRequestFileDetails> response = new SingleResponse<ApiRequestFileDetails>();

    try
    {
        fileDetails.DeleteFile(id);
        response.Message = ResponseConstrains.RESULT_SUCCESS;
        response.Data = null;
        response.StatusCode = (int)HttpStatusCode.OK; ;
    }
    catch (Exception ex)
    {
        response.Data = null;
        response.Message = ResponseConstrains.RESULT_FAIL;
        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
    }

    return Results.Ok(response);
});

app.MapGet("/api/getfile", [AllowAnonymous] async (int id, IFileDetails fileDetails) =>
{
    SingleResponse<ApiRequestFileDetails> response = new SingleResponse<ApiRequestFileDetails>();

    try
    {
        var res = await fileDetails.GetFile(id);
        response.Message = "Success";
        response.Data = res;
        response.StatusCode = 200;
    }
    catch (System.Exception ex)
    {
        response.Data = null;
        response.Message = ResponseConstrains.RESULT_FAIL;
        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        throw;
    }
    return Results.Ok(response);
});

app.MapGet("/api/getfileall", [AllowAnonymous] async (IFileDetails fileDetails) =>
{
    ListResponse<ApiRequestFileDetails> response = new ListResponse<ApiRequestFileDetails>();

    try
    {
        var res = await fileDetails.GetFileAll();
        response.Message = ResponseConstrains.RESULT_SUCCESS;
        response.Data = res;
        response.StatusCode = (int)HttpStatusCode.OK;
    }
    catch (System.Exception ex)
    {
        response.Data = null;
        response.Message = ResponseConstrains.RESULT_FAIL;
        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        throw;
    }
    return Results.Ok(response);
});

#endregion


app.Run();
