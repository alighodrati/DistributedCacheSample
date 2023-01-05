////dotnet sql-cache create "Data Source=.;Initial Catalog=sqlcache;Integrated Security=True;" dbo TestCache
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.SqlServer;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis-Cache");
    options.InstanceName = "SampleInstance";
});
//builder.Services.AddSqlServerCache(options =>
//{
//    options.ConnectionString = builder.Configuration.GetConnectionString("Sql-Cache");
//    options.SchemaName = "dbo";
//    options.TableName = "TestCache";
//});
//builder.Services.AddDistributedMemoryCache();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Stopwatch stopwatch1 = new Stopwatch();
stopwatch1.Start();
Console.WriteLine("cachemodel generating...");
var _cachemdel = new CacheModel();
stopwatch1.Stop();
Console.WriteLine(string.Format("cachemodel generated ====>{0}", stopwatch1.ElapsedMilliseconds));


// Configure the HTTP request pipeline.


app.MapGet("/benchmark", () =>
{
#error add desrilizer benchmark
    var stringBuilder = new StringBuilder();


    Stopwatch stopwatch = new Stopwatch();

    stopwatch.Start();
    var neton = JsonConvert.SerializeObject(_cachemdel);
    stopwatch.Stop();
    stringBuilder.AppendLine($" newtonsoftjson.SerializeObject {stopwatch.ElapsedMilliseconds}");

    stopwatch.Restart();
    var textjson = System.Text.Json.JsonSerializer.Serialize(_cachemdel);
    stopwatch.Stop();
    stringBuilder.AppendLine($" system.text.json.Serialize {stopwatch.ElapsedMilliseconds}");

    stopwatch.Restart();
    var @byte = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(_cachemdel);
    stopwatch.Stop();
    stringBuilder.AppendLine($" system.text.json.SerializeToUtf8Bytes {stopwatch.ElapsedMilliseconds}");

    stopwatch.Restart();
    var @bytestring = Encoding.Unicode.GetBytes(System.Text.Json.JsonSerializer.Serialize(_cachemdel));
    stopwatch.Stop();
    stringBuilder.AppendLine($" system.text.json.SerializeToUtf8Bytes {stopwatch.ElapsedMilliseconds}");


    return new { benchmark = stringBuilder.ToString(), neton, textjson, @byte, @bytestring };
});
app.MapGet("/redis/byte", async ([FromServices] RedisCache cache, HttpContext context) =>
{
    var guid = await cache.SetAsync(context.Request.Path, _cachemdel);
    var get = await cache.GetByByteAsync(guid);
    return new { guid,get };
});
app.MapGet("/redis/string", async ([FromServices] RedisCache cache, HttpContext context) =>
{
    var guid = await cache.SetStringAsync(context.Request.Path, _cachemdel);
    var get = await cache.GetByStringAsync(guid);
    return new { guid, get };
});

app.MapGet("/sql/byte", async ([FromServices] SqlServerCache cache, HttpContext context) =>
{
    var guid = await cache.SetAsync(context.Request.Path, _cachemdel);
    var get = await cache.GetByByteAsync(guid);
    return new { guid, get };
});
app.MapGet("/sql/string", async ([FromServices] SqlServerCache cache, HttpContext context) =>
{
    var guid = await cache.SetStringAsync(context.Request.Path, _cachemdel);
    var get = await cache.GetByStringAsync(guid);
    return new { guid, get };
});

app.MapGet("/memory/byte", async ([FromServices] IDistributedCache cache, HttpContext context) =>
{
    var guid = await cache.SetAsync(context.Request.Path, _cachemdel);
    var get = await cache.GetByByteAsync(guid);
    return new { guid, get };
});
app.MapGet("/memory/string", async ([FromServices] IDistributedCache cache, HttpContext context) =>
{
    var guid = await cache.SetStringAsync(context.Request.Path, _cachemdel);
    var get = await cache.GetByStringAsync(guid);
    return new { guid, get };
});


app.Run();
