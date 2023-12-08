module AoC2023.Day08

let lines = System.IO.File.ReadLines "08/data" |> Seq.toArray
let instructions = lines[0]
let addMapping map (s:string) = Map.add s[0..2] (s[7..9],s[12..14]) map
let map = lines |> Array.skip 2 |> Array.fold addMapping Map.empty<string,string*string>

let rec traverse step (instructions:string) map loc =
    match instructions[step % instructions.Length] with
    | _ when loc = "ZZZ" -> step
    | 'L' -> traverse (step + 1) instructions map (Map.find loc map |> fst)
    | 'R' -> traverse (step + 1) instructions map (Map.find loc map |> snd)

let part1 () = traverse 0 instructions map "AAA" |> printfn "%A"

let startNodes = map |> Map.keys |> Seq.where (fun s -> s[2] = 'A') |> Seq.toArray
let rec spookyTraverse found step (instructions:string) map (loc:string) =
    match instructions[step % instructions.Length] with
    | _ when loc[2] = 'Z' && found -> step
    | 'L' when loc[2] = 'Z' -> spookyTraverse true 0 instructions map (Map.find loc map |> fst)
    | 'R' when loc[2] = 'Z' -> spookyTraverse true 0 instructions map (Map.find loc map |> snd)
    | 'L' -> spookyTraverse found (step + 1) instructions map (Map.find loc map |> fst)
    | 'R' -> spookyTraverse found (step + 1) instructions map (Map.find loc map |> snd)

let initSteps = startNodes |> Array.map (spookyTraverse true 0 instructions map)
let loopSteps = startNodes |> Array.map (spookyTraverse false 0 instructions map)

let rec find (steps:int[]) (i:int) =
    if steps |> Array.forall ((<>) -1) then steps |> Array.min
    else find (steps |> Array.map (fun s -> if s = -1 then -1 else s + i)) (i + 1)

let part2 () =  1 |> printfn "%A"

// 21409; 16531; 19241; 19783; 14363; 15989
// 19512; 2439; 2981; 1897; 11924; 8672
