using Grpc.Core;
using Serilog;

namespace Grpc.Configuration
{
    public class LoggerMiddleware
    {
        private const string _seqUrl = "http://localhost:5341";

        private readonly RequestDelegate _next;

        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Application", "Rest")
                .WriteTo.Console()
                .WriteTo.Seq(_seqUrl)
                .CreateLogger();

            Log.Information("Receiving request. Type/Method: {Type} / {Method}", MethodType.Unary, context.Request.Method);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error thrown by {context.Request.Path}.");
                throw new Exception($"Error thrown by {context.Request.Path}.");
            }

            Log.CloseAndFlush();
        }
    }
}
