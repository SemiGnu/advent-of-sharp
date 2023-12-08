module AoC2023.Day08

let lines = System.IO.File.ReadLines "08/data" |> Seq.toArray
let instructions = lines[0]
let addMapping map (s:string) = Map.add s[0..2] (s[7..9],s[12..14]) map
let map = lines |> Array.skip 2 |> Array.fold addMapping Map.empty<string,string*string>

let rec traverse step loc (instructions:string) map =
    match instructions[step % instructions.Length] with
    | _ when loc = "ZZZ" -> step
    | 'L' -> traverse (step + 1) (Map.find loc map |> fst) instructions map
    | 'R' -> traverse (step + 1) (Map.find loc map |> snd) instructions map

let part1 () = traverse 0 "AAA" instructions map |> printfn "%A"

let startNodes = map |> Map.keys |> Seq.where (fun s -> s[2] = 'A') |> Seq.toArray
let rec spookyTraverse step (locs: string array) (instructions:string) map =
    match instructions[step % instructions.Length] with
    | _ when locs |> Seq.forall (fun s -> s[2] = 'Z') -> step
    | 'L' -> spookyTraverse (step + 1) (Array.map (fun loc -> Map.find loc map |> fst) locs) instructions map
    | 'R' -> spookyTraverse (step + 1) (Array.map (fun loc -> Map.find loc map |> snd) locs) instructions map
    
let part2 () = spookyTraverse 0 startNodes instructions map |> printfn "%A"
