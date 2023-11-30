module AoC2023.Helpers

let getTest day = System.IO.File.ReadLines $"%02i{day}\\test.txt" |> List.ofSeq
let getData day = System.IO.File.ReadLines $"%02i{day}\\data.txt" |> List.ofSeq
