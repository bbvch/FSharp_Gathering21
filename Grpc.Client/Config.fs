module Grpc.Client.Config

open FSharp.Data
open System


[<Literal>]
let private LaunchSettingsPath =
  __SOURCE_DIRECTORY__
  + "/../Grpc.Server/Properties/launchSettings.json"

type private LaunchSettings = JsonProvider<LaunchSettingsPath>

let ServerUri =
  LaunchSettings
    .GetSample()
    .Profiles.GrpcServer.ApplicationUrl.Split(';')
  |> Seq.map Uri
  |> Seq.find (fun u -> u.Scheme = "https")
