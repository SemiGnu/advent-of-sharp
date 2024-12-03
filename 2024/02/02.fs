module AoC2024.Day02

open System

let lines = System.IO.File.ReadLines "02/test"

let reports = lines |> Seq.map (fun s -> s.Split ' ' |> Array.map int) |> Array.ofSeq
let increments report = (Array.take (Array.length report - 1) report, Array.skip 1 report) ||> Array.map2 (fun x y -> x - y)
let isSafe incs = (Array.forall (fun x -> x < 0) incs || Array.forall (fun x -> x > 0) incs) && Array.forall (fun (x: int) -> Math.Abs(x) < 4) incs
let safeReports = reports |> Array.filter (increments >> isSafe)
let part1 () = safeReports |> Array.length |> printfn "%A"

let isEvenSafer incs = (incs |> Array.filter (fun x -> x < 0) |> Array.length < 2 || incs |> Array.filter (fun x -> x > 0) |> Array.length < 2) && incs |> Array.filter (fun (x: int) -> Math.Abs(x) > 3) |> Array.length < 2
let evenSaferReports = reports |> Array.filter (increments >> isEvenSafer)
let part2 () = evenSaferReports |> Array.length |> printfn "%A"
