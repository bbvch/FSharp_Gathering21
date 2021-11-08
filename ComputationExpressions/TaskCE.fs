module TaskCE

open System.Threading.Tasks

module ConfigureAwaitFalse =
  open FSharp.Control.Tasks.Affine

  let t = task { do! Task.Delay 42 }

module ConfigureAwaitTrue =
  open FSharp.Control.Tasks.NonAffine

  let t = task { return! Task.FromResult 42 }
