module _05

open System.Text.RegularExpressions

// PARSING 

let lines = Seq.toList <| System.IO.File.ReadLines("05/data.txt")

type vector = { x1:int; y1:int; x2:int; y2:int }

let parseLine line = 
    let g = Regex("(\d{1,3}),(\d{1,3}) -> (\d{1,3}),(\d{1,3})").Match(line).Groups
    in { x1 = int g[1].Value; y1 = int g[2].Value; x2 = int g[3].Value; y2 = int g[4].Value }

let vectors = lines |> List.map parseLine

// PART 1

let maxX = vectors |> List.map (fun v -> max v.x1 v.x2) |> List.max
let maxY = vectors |> List.map (fun v -> max v.y1 v.y2) |> List.max

let grid = {1..maxY} |> Seq.toList |> List.map (fun _ -> {1..maxX} |> Seq.toList |> List.map (fun _ -> 0))

let pointsX v = {(v.x1)..(if v.x2 < v.x1 then -1 else 1)..(v.x2)} |> Seq.toList
let pointsY v = {(v.y1)..(if v.y2 < v.y1 then -1 else 1)..(v.y2)} |> Seq.toList

let vectorToPoints v = 
    if v.x1 = v.x2
    then pointsY v |> List.map (fun y -> (v.x1, y))
    else pointsX v |> List.map (fun x -> (x, v.y1))

let points = vectors |> List.filter (fun v -> v.x1 = v.x2 || v.y1 = v.y2) |> List.map vectorToPoints |> List.fold (fun ps p-> p @ ps) []

let groups = List.groupBy (fun x -> x) points |> List.map (snd >> List.length) |> List.filter (fun x -> x > 1) |> List.length

let part1 = sprintf "%d" groups

// PART 2

let vectorToPointsDiagonal v = 
    if v.x1 <> v.x2 && v.y1 <> v.y2
    then List.zip (pointsX v) (pointsY v) 
    else vectorToPoints v


let points2 = vectors |> List.map vectorToPointsDiagonal |> List.fold (fun ps p-> p @ ps) []

let groups2 = List.groupBy (fun x -> x) points2 |> List.map (snd >> List.length) |> List.filter (fun x -> x > 1) |> List.length

let part2 = sprintf "%d" groups2