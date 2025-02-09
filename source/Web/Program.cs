var builder = WebApplication.CreateBuilder();

builder.Host.UseSerilog();

builder.Services.AddResponseCompression();
builder.Services.AddJsonStringLocalizer();


builder.Services.AddSwaggerDefault();
builder.Services.AddControllers().AddJsonOptions();

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