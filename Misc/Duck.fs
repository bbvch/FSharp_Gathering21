module Duck

let inline add x y = x + y

let inline quack duck =
  (^N : (member Say : string) duck)

type Duck() =
  member _.Say = "Quack"

Duck() |> quack |> printfn "Hello %s"
