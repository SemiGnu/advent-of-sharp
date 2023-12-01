module AoC2023.Day01

open AoC2023
open System

// let lines = Helpers.getTest 1 (Some 3)
let lines = Helpers.getData 1

let toNumber s = (Seq.find Char.IsDigit s |> int |> (+) -48 ) * 10 + (Seq.rev s |> Seq.find Char.IsDigit |> int |> (+) -48)
let part1 () = Seq.map toNumber lines |> Seq.sum |> printfn "%i"

let realNumbers = seq {1..9} |> Seq.map string
let stringNumbers = seq ["one"; "two"; "three"; "four"; "five"; "six"; "seven"; "eight"; "nine"]
let allNumbers = Seq.concat [ realNumbers; stringNumbers ]
let hmm = (<=) 0

let toDigit (number: string) = Seq.findIndex number.Equals allNumbers |> fun n -> n % 9 + 1
let firstNumber (line: string) = Seq.minBy (fun (n: string) -> line.IndexOf n |> uint) allNumbers |> toDigit
let lastNumber (line: string) = Seq.maxBy (fun (n: string) -> line.LastIndexOf n) allNumbers |> toDigit
let toNumber2 line = (firstNumber line) * 10 + lastNumber line
let part2 () = Seq.map toNumber2 lines
               |> Seq.sum
               |> printfn "%i"
