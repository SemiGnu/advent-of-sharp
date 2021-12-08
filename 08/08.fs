module _08

// PARSING

let lines = System.IO.File.ReadLines("08/data.txt") |> Seq.toList

let displays = lines |> List.map (fun line -> let l = line.Split " | " |> Array.map (fun display -> display.Split " " |> Array.toList) in (l[0], l[1]) ) 

// PART 1

let easyDigits = [2;3;4;7]

let easy = displays |> List.map (fun output -> snd output |> List.filter (fun digits -> List.contains digits.Length easyDigits) |> List.length) |> List.sum

let part1 = sprintf "%d" <| easy

// PART 2

let part2 = sprintf "%d" <| 0
