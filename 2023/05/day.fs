module AoC2023.Day05

let lines = System.IO.File.ReadLines "05/data" |> List.ofSeq
type Mapping = {target:uint; source:uint; range:uint}
let parseMapping (line:string) = let ns = line.Split " " |> Array.map uint in {target = ns[0];source = ns[1]; range = ns[2] }
let safeSkip n list = if List.length list > n then List.skip n list else []
let rec splitBy pred seq = match seq with
                           | [] -> []
                           | _ -> let heads = Seq.takeWhile (not << pred) seq |> Seq.length in
                                  [List.take heads seq] @ splitBy pred (safeSkip (heads + 1) seq)

let rec getTarget (source:uint) mappings =
    match mappings with
    | [] -> source
    | m::ms -> if source >= m.source && source < m.source + m.range
               then m.target + source - m.source
               else getTarget source ms
let getLocation maps source  = List.fold getTarget source maps

let part1 () =
    let seeds = (lines[0][7..]).Split(" ") |> Array.map uint in
    let maps = lines |> List.skip 2 |> splitBy (fun (s:string) -> s = "")  |> List.map (List.tail >> List.map parseMapping) in
    seeds |> Array.map (getLocation maps) |> Array.min |> printfn "%A"

let part2 () =
    let seeds = (lines[0][7..]).Split(" ") |> Array.map uint in
    let maps = lines |> List.skip 2 |> splitBy (fun (s:string) -> s = "")  |> List.map (List.tail >> List.map parseMapping) in
    seeds |> Array.map (getLocation maps) |> Array.min |> printfn "%A"
