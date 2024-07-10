using Grpc.Core;
using Grpc.Core.Interceptors;
using Serilog;

namespace Grpc.Configuration
{
    public class ServerLoggerInterceptor : Interceptor
    {
        public ServerLoggerInterceptor()
        {
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Application", "Grpc")
                .WriteTo.Console()
                .WriteTo.Seq("https://localhost:5341")
                .CreateLogger();

            Log.Information("Starting receiving call. Type/Method: {Type} / {Method}", MethodType.Unary, context.Method);

            Log.CloseAndFlush();

            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error thrown by {context.Method}.");
                throw;
            }
        }
    }
}
