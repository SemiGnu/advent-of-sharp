module _07

// PARSING 

let line =  System.IO.File.ReadLines("07/data.txt") |> Seq.head

let postitions = line.Split "," |> Array.map int |> Array.toSeq

// PART 1

let min = Seq.min postitions
let max = Seq.max postitions

let option ps d = ps |> Seq.map (fun p -> abs (p-d)) |> Seq.sum

let options ps = {min..max} |> Seq.map (option ps)

let part1 = sprintf "%d" <| (Seq.min <| options postitions)

// PART 2

let option2 ps d = ps |> Seq.map (fun p -> let n = abs (p-d) in n * (n+1) / 2) |> Seq.sum

let options2 ps = {min..max} |> Seq.map (option2 ps)

let part2 = sprintf "%d" <| (Seq.min <| options2 postitions)
