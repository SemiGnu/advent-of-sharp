module AoC2023.Day01

open System

let lines = System.IO.File.ReadLines "01/data.txt"

let toNumber s = (Seq.find Char.IsDigit s |> (string >> int) ) * 10 + (Seq.rev s |> Seq.find Char.IsDigit |> (string >> int))
let part1 () = Seq.map toNumber lines |> Seq.sum |> printfn "%i"

let digitNumbers = seq {1..9} |> Seq.map string
let stringNumbers = seq ["one"; "two"; "three"; "four"; "five"; "six"; "seven"; "eight"; "nine"]
let allNumbers = Seq.concat [ digitNumbers; stringNumbers ]
let toDigit (number: string) = (Seq.findIndex number.Equals allNumbers) % 9 + 1
let firstDigit (line: string) = allNumbers |> Seq.minBy (line.IndexOf >> uint) |> toDigit
let lastDigit (line: string) = allNumbers |> Seq.maxBy line.LastIndexOf |> toDigit
let toNumber2 line = (firstDigit line) * 10 + lastDigit line
let part2 () = Seq.map toNumber2 lines |> Seq.sum |> printfn "%i"
