module SeqCE

let myEnumerable = seq {
  yield 42
  yield! [| 7; 666 |]
}
