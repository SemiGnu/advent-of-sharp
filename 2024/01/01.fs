module AoC2024.Day01

open System

let lines = System.IO.File.ReadLines "01/data.txt"
let pair (line: string) = line.Split "   " |> Array.map int |> fun a -> (a[0], a[1])
let values = lines |> Seq.toList |> List.map pair |> List.unzip
let totalDistance = values |> fun (xs, ys) -> (List.sort xs, List.sort ys) ||> List.map2 (fun a b -> Math.Abs(a - b)) |> List.sum
let part1 () = totalDistance |> printfn "%i"

let histogram = values |> snd |> List.groupBy id |> Map.ofList
let newDistance = values |> fst |> List.filter (fun x -> Map.containsKey x histogram) |> List.fold (fun total key -> total + histogram[key].Length * key) 0
let part2 () = newDistance |> printfn "%i"
