open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open Google.Protobuf
open System.IO
open System.Text.Json
open System.Threading.Tasks

type HelloJson = { Name: string }

type Serialization() =

  [<Benchmark(Baseline = true)>]
  member _.Json() =
    let r = { Name = "Foo" }
    use ms = new MemoryStream()
    let t = JsonSerializer.SerializeAsync(ms, r)
    if t.Status <> TaskStatus.RanToCompletion then
      failwith "Expected synchronous completion"

  [<Benchmark>]
  member _.Protobuf() =
    let r = HelloRequest(Name = "Foo")
    use ms = new MemoryStream()
    r.WriteTo ms

[<EntryPoint>]
let main argv =
  BenchmarkRunner.Run<Serialization>(null, argv)
  |> ignore
  0
