module _10

// PARSING

let lines = System.IO.File.ReadLines("10/data.txt") |> Seq.map Seq.toList

// PART 1

let pairs = [ '(',')';'[',']';'{','}';'<','>' ] |> Map.ofList
let scores = [ ')',3;']',57;'}',1197;'>',25137 ] |> Map.ofList

let rec chunky chars = 
    match chars with
    | x::y::xs -> if pairs.ContainsKey x && pairs[x] = y then xs else x::(chunky (y::xs))
    | xs -> xs

let rec simplify chars = let inner = chunky chars in if chars.Length = inner.Length then chars else simplify inner

let rec score (chars:char list) = let opt = chars |> List.tryFind scores.ContainsKey in if opt.IsSome then scores[opt.Value] else 0

let part1 = sprintf "%d" <| (lines |> Seq.map simplify |> Seq.map score |> Seq.sum)

// PART 2

let newScores = [ ')',1L;']',2L;'}',3L;'>',4L ] |> Map.ofList

let newScore chars = chars |> Seq.map (fun c -> pairs[c]) |> Seq.rev |> Seq.fold (fun s c -> s * 5L + newScores[c]) 0L

let part2 = sprintf "%d" <| let scores = lines |> Seq.map simplify |> Seq.filter (not << List.exists (not << pairs.ContainsKey)) |> Seq.map newScore |> Seq.sort |> Seq.toArray in scores[scores.Length / 2] 
