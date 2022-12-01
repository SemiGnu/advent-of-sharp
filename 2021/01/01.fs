module _01

// PARSING

let lines = System.IO.File.ReadLines("01/data.txt")

let readings = lines |> Seq.map int |> Seq.toList

// PART 1

let hm = readings |> List.skip 3 |> Seq.zip readings

let increases readings setSize = readings |> List.skip setSize |> Seq.zip readings |> Seq.map (fun p -> if fst p < snd p then 1 else 0) |> Seq.sum

let part1 = sprintf "%d" <| increases readings 1

// PART 2

let part2 = sprintf "%d" <| increases readings 3
