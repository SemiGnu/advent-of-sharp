module AoC2023.Day03

open System
let lines = System.IO.File.ReadLines "03/test" |> Seq.toList

// 4361
// 467835

type Number = {
    x:int
    y:int
    value:int
    length:int
}

type Part = {
    x:int
    y:int
    value:Char
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
let isPart c =  not <| (Char.IsDigit(c) || c.Equals('.'))

let parseParts lines = lines |> Seq.mapi (fun y line -> line |> Seq.mapi (fun x char -> if (isPart char) then Some {x = x; y = y; value = char} else None) |> Seq.choose id ) |> Seq.concat |> Seq.toList

let isAdjacent part (number:Number) = abs (part.y - number.y) <= 1 && (abs (part.x - number.x) <= 1 || abs (part.x - number.x - number.length + 1) <= 1)

let partNumbers (numbers: Number list) (parts: Part list) = parts |> Seq.map (fun part -> (part, numbers |> Seq.where (isAdjacent part)))

let part1 () = ((parseLines [] lines 0), (parseParts lines)) ||> partNumbers
               |> Seq.sumBy (snd >> Seq.sumBy (fun (n:Number) -> n.value)) |> printfn "%i"
let part2 () = ((parseLines [] lines 0), (parseParts lines)) ||> partNumbers
               |> Seq.where (fun np -> (fst np).value = '*' && (Seq.length (snd np)) = 2)
               |> Seq.map snd |> Seq.sumBy (Seq.toList >> (fun ns -> ns[0].value * ns[1].value)) |> printfn "%i"
