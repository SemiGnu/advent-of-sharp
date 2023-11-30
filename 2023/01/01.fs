module AoC2023.Day01

open AoC2023

let numberOfIncreases range nums =
    Seq.skip range nums |> Seq.zip nums |> Seq.filter (fun (a, b) -> b > a ) |> Seq.length

let part1 (lines:seq<string>) = Seq.map int lines |> numberOfIncreases 1 |> printfn "%i"
let part2 (lines:seq<string>) = Seq.map int lines |> numberOfIncreases 3 |> printfn "%i"
