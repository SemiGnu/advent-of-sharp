module AoC2023.Helpers

let getTest day n =
    match n with
    | Some n ->  System.IO.File.ReadLines $"%02i{day}\\test{n}.txt" |> List.ofSeq
    | None ->  System.IO.File.ReadLines $"%02i{day}\\test.txt" |> List.ofSeq

let getData day = System.IO.File.ReadLines $"%02i{day}\\data.txt" |> List.ofSeq
