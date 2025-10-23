using Microsoft.Extensions.Logging.EventLog;
using Microsoft.Extensions.Logging.EventSource;
using Microsoft.Extensions.Logging.TraceSource;

var builder = WebApplication.CreateBuilder(args);

// ----------------- Logging Configuration -----------------
builder.Logging.ClearProviders(); // Removes default providers (Console, Debug, etc.)

// 1️⃣ EventSource provider - for ETW/EventPipe tracing
builder.Logging.AddEventSourceLogger();

// 2️⃣ EventLog provider - logs to Windows Event Viewer (Windows only)
if (OperatingSystem.IsWindows())
{
    builder.Logging.AddEventLog(new EventLogSettings
    {
        SourceName = "BuiltInLoggingDemoApp",
        LogName = "Application"
    });
}

// 3️⃣ TraceSource provider - for System.Diagnostics trace output
builder.Logging.AddTraceSource(
    new System.Diagnostics.SourceSwitch("sourceSwitch", "Verbose"),
    new System.Diagnostics.DefaultTraceListener()
);

// Optional: also log to console for easy testing
builder.Logging.AddConsole();

// ----------------------------------------------------------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
