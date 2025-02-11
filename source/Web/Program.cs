using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder();

builder.Host.UseSerilog();

builder.Services.AddResponseCompression();
builder.Services.AddJsonStringLocalizer();

builder.Services.AddContext<Context>(options => options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(Context))));
builder.Services.AddClassesMatchingInterfaces(nameof(EventNet));
builder.Services.AddMediator(nameof(EventNet));
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var application = builder.Build();

application.UseException();
application.UseHsts().UseHttpsRedirection();
application.UseResponseCompression();
application.UseStaticFiles();
application.UseSwagger().UseSwaggerUI();
application.UseRouting();
application.MapControllers();
application.MapFallbackToFile("index.html");

application.Run();