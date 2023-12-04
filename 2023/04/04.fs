module AoC2023.Day04

let lines = System.IO.File.ReadLines "04/test"

// 13

let parseCard (line:string) = (line.Split(":")[1]).Split("|") |> Seq.map (fun (s:string) -> s.Split(" ", System.StringSplitOptions.RemoveEmptyEntries) |> Seq.map int |> Set.ofSeq)
let scoreCard = Set.intersectMany >> Set.count >> fun n -> pown 2 (n - 1)

let part1 () = lines |> Seq.sumBy (parseCard >> scoreCard) |> printfn "%i"
let part2 () = lines |> Seq.iter (printfn "%s")
