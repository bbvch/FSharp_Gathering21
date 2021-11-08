open Grpc.Net.Client
open Grpc.Core
open Grpc.Client

[<EntryPoint>]
let main argv =
  use channel = GrpcChannel.ForAddress(Config.ServerUri)
  let client = Greeter.GreeterClient(channel)
  let elho =
    try
      let response = HelloRequest(Name = "F#") |> client.SayHello
      response.Message
    with
    | :? RpcException as e when e.StatusCode = StatusCode.Unimplemented -> "NOO"
  printfn $"Server says '%s{elho}'"
  0
