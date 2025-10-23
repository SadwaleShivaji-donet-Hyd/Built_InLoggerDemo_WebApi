BuiltInLoggingDemo — ASP.NET Core Web API with Built-in Logging Providers
Overview

This project demonstrates how to use ASP.NET Core built-in logging providers:

EventSource — for ETW/EventPipe tracing

EventLog — writes to Windows Event Viewer

TraceSource — integrates with System.Diagnostics.Trace

A simple StudentController is used to generate log messages at various levels (Information, Warning, Error, etc.).


**Project Structure**
BuiltInLoggingDemo/
│
├── Controllers/
│   └── StudentController.cs     # Web API controller for demo
│
├── Program.cs                   # Main configuration for built-in logging
├── appsettings.json             # Logging level configuration
├── BuiltInLoggingDemo.csproj
└── README.md                    # This file



| Provider                 | Description                                                           | Platform     |
| ------------------------ | --------------------------------------------------------------------- | ------------ |
| `AddEventLog()`          | Writes logs to Windows Event Viewer.                                  | Windows only |
| `AddTraceSource()`       | Uses `System.Diagnostics.TraceSource` for legacy tracing integration. | All OS       |
| `AddConsole()`           | Displays logs in console for development convenience.                 | All OS       |

