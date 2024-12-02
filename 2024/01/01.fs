module AoC2024.Day01

open System

let lines = System.IO.File.ReadLines "01/data.txt"
let pair (line: string) = line.Split "   " |> Array.map int |> fun a -> (a[0], a[1])
let values = lines |> Seq.toList |> List.map pair |> List.unzip
// let values = lines |> Seq.toList |> List.map pair |> List.fold (fun (xs, ys) (x,y) -> (x::xs,y::ys)) ([],[])
let totalDistance = values |> fun (xs, ys) -> (List.sort xs, List.sort ys) ||> List.map2 (fun a b -> Math.Abs(a - b)) |> List.sum

let part1 () = totalDistance |> printfn "%i"

let digitNumbers = [1..9]|> List.map string
let stringNumbers = ["one"; "two"; "three"; "four"; "five"; "six"; "seven"; "eight"; "nine"]
let allNumbers =  digitNumbers @ stringNumbers
let toDigit (number: string) = (Seq.findIndex number.Equals allNumbers) % 9 + 1
let firstDigit (line: string) = allNumbers |> Seq.minBy (line.IndexOf >> uint) |> toDigit
let lastDigit (line: string) = allNumbers |> Seq.maxBy line.LastIndexOf |> toDigit
let toNumber2 line = (firstDigit line) * 10 + lastDigit line
let part2 () = lines |> Seq.sumBy toNumber2 |> printfn "%i"
