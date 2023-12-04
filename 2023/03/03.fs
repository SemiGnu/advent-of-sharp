module AoC2023.Day03

open System
let lines = System.IO.File.ReadLines "03/test" |> Seq.toList

type Number = {
    x:int
    y:int
    value:int
    length:int
}
let addNumber numbers (number:string) x y = {x = x; y = y; value = int number; length = number.Length} :: numbers
let rec parseLine (line:string) (numbers:Number list) (x:int) (y:int) =
    match line with
    | "" -> numbers
    | _ when not (Char.IsDigit line[0]) -> parseLine line[1..] numbers (x+1) y
    | _ -> let l = Seq.length <| Seq.takeWhile Char.IsDigit line in
           parseLine line[l..] (addNumber numbers line[..l-1] x y) (x + l) y
           
let rec parseLines numbers lines y  =
    match lines with
    | [] -> numbers
    | line :: tail -> parseLine line [] 0 y @ parseLines numbers tail (y+1)
let isPart x y = let c = lines[y][x] in Char.IsDigit(c) || c.Equals('.') |> not


let test () = parseLines [] lines 0  |> Seq.length |> printfn "%A"
let part1 () = lines |> Seq.iter (printfn "%s")
let part2 () = lines |> Seq.iter (printfn "%s")
