module AoC2023.Day03

open System
let lines = System.IO.File.ReadLines "03/test" |> Seq.toArray

let isPart x y = let c = lines[y][x] in Char.IsDigit(c) || c.Equals('.') |> not

let nLine (x:int) (y:int) l = Seq.zip (Seq.init l (fun i -> i + x)) (Seq.init l (fun _ -> y)) |> Seq.toArray

let getNeighbors x y l = Seq.concat [
    nLine (x-1) (y-1) (l+2);
    nLine (x-1) ( y ) (l+2);
    nLine (x-1) (y+1) (l+2);
]

let test () = (2,1) ||> isPart |> printfn "%b"
let part1 () = lines |> Seq.iter (printfn "%s")
let part2 () = lines |> Seq.iter (printfn "%s")
