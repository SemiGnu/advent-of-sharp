﻿module AoC2023.Day02

let lines = System.IO.File.ReadLines "02/data"

let parseCube (cube:string) = match cube.Split(" ") with
                              | [| n; "red" |] -> [|(int n); 0; 0|]
                              | [| n; "green" |] -> [|0; (int n); 0|]
                              | [| n; "blue" |] -> [|0; 0; (int n)|]
                              | _ -> [|0; 0; 0|]
let parseDraw (draw:string) = draw.Split(",", System.StringSplitOptions.TrimEntries) 
                              |> Seq.map parseCube
                              |> Seq.fold (Seq.map2 (+)) [|0;0;0|]
                              |> Seq.toArray

let parseGame (game:string) = (game.Split(":")[1]).Split(";", System.StringSplitOptions.TrimEntries) 
                              |> Seq.map parseDraw
                              |> Seq.toArray

let possible draw = draw |> Seq.fold2 (fun s m d -> s && m >= d) true [|12;13;14|]
let part1 () = lines |> Seq.map parseGame  |> Seq.mapi (fun i game -> if (Seq.forall possible game) then i + 1 else 0) |> Seq.sum |> printfn "%i"

let minPossible =  Seq.fold (Seq.map2 max) [|0;0;0|]
let part2 () = lines |> Seq.sumBy (parseGame >> minPossible >> Seq.fold (*) 1) |> printfn "%i"
