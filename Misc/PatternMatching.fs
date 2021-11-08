module Misc.PatternMatching

open System
open System.Drawing

// 'let' does pattern matching
let success, value = Int32.TryParse("42")
let (a, b) = (4.2, 6)
// parentheses are optional:
let a', b' = "a", 3

let authenticate input =
    match input with
    | "Hello" | "world" -> true
    | msg -> msg.StartsWith "pwd"

// pattern matching is omnipresent, so there is a shorthand
// which also saves one level of indentation
let authenticate' = function
| "Hello" | "world" -> true
| msg -> msg.StartsWith "pwd"

// the most basic pattern is a single case which always succeeds:
let (|UpperCase|) (x: string) = x.ToUpperInvariant()

// either match or fail:
let (|IsColor|_|) c =
    let candidate = Color.FromName c
    if candidate.IsKnownColor then Some(candidate) else None

// Choose an option:
let (|Even|Odd|) n = if n % 2 = 0 then Even else Odd

// Pass parameters:
let (|RegexContains|_|) pattern input =
    let matches = System.Text.RegularExpressions.Regex.Matches(input, pattern)
    if matches.Count > 0 then Some [ for m in matches -> m.Value ]
    else None

let GetColor = function
| IsColor c -> c // this matches only when IsColor returns Some(color). The Color is extracted automatically.
| UpperCase "SKY" -> Color.Azure // The returned uppercase string is matched against "SKY".
| _ -> Color.Transparent

let colorData : obj[] list =
    [ [| "Deep"; Color.Transparent |]
      [| "sKy"; Color.Azure |]
      [| "Purple"; Color.Purple |] ]

let unicodeCategoryCount = Seq.length << Seq.distinctBy Char.GetUnicodeCategory

let (|IsStrongPassword|_|) (pwd: string) =
    if pwd.Length > 8 && unicodeCategoryCount pwd >= 3 then Some()
    else None

let numberFromString = Seq.filter Char.IsDigit >> String.Concat >> Int32.Parse

// A contrived example of patterns of pattern matching :-) we have seen so far:
let guessNumericPassword = function
// plus so called GUARDs: using `when` we can further restrict patterns:
| IsColor c when c <> Color.Transparent -> c.ToArgb()
| IsStrongPassword as pwd -> pwd |> numberFromString
| RegexContains "^\d{1,8}$" [number] -> number |> Int32.Parse
| UpperCase "PASSWORD" | "12345678" -> 12345678
| _ -> failwith "Could not guess password"

let ``patterns are tested for exhaustiveness`` = function
// try commenting any line and watch the squigglies ^^^
| Even -> -1
| Odd -> 1
