open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Builder

type Startup() =
  member _.ConfigureServices(services: IServiceCollection) = services.AddGrpc() |> ignore

  member _.Configure(app: IApplicationBuilder) =
    app
      .UseRouting()
      .UseEndpoints(fun e -> e.MapGrpcService<Greeter.GreeterBase>() |> ignore)
    |> ignore

[<EntryPoint>]
let main argv =
  HostBuilder()
    .ConfigureAppConfiguration(fun c -> c.AddCommandLine(argv) |> ignore)
    .ConfigureWebHostDefaults(fun b ->
      b
        .UseStartup<Startup>()
        .UseKestrel(fun k -> k.AddServerHeader <- false)
      |> ignore)
    .RunConsoleAsync()
    .GetAwaiter()
    .GetResult()

  0
