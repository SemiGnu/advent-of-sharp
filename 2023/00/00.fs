module AoC2023.Day00

open AoC2023

let dayNum = 0

let printLines lines = 
    for line in lines do
        printfn $"{line}"

let part1 () = Helpers.getTest dayNum |> printLines
let part2 () = Helpers.getData dayNum |> printLines
