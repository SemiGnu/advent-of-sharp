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
let getIndices (line: string) = Seq.map (fun (n: string) -> (line.IndexOf n, line.LastIndexOf n)) allNumbers
                                |> Seq.mapi (fun i x -> (x, i % 9 + 1))
                                |> Seq.filter (fst >> fst >> (<=) 0)
let toNumber2 indices = (Seq.minBy (fst>>fst) indices |> snd) * 10
                        + (Seq.maxBy (fst>>snd) indices |> snd)
let part2 () = Seq.map getIndices lines
               |> Seq.map toNumber2
               |> Seq.sum
               |> printfn "%i"
