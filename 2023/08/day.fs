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

let initSteps = startNodes |> Array.map (spookyTraverse true 0 instructions map >> int64)
let loopSteps = startNodes |> Array.map (spookyTraverse false 0 instructions map >> int64)

let indexOfLowest (steps: int64 array) = Array.findIndex ((=) (Array.min steps)) steps
let lowArray (steps: int64 array) =
    let min = indexOfLowest steps
    loopSteps |> Array.mapi (fun i s -> if i = min then s else 0)

let rec incrementLowest (steps: int64 array) =
    if steps |> Array.forall ((=) steps[0])
    then steps[0]
    else if steps |> Array.forall ((=) 0)
    then 0
    else steps |> lowArray |> Array.map2 (+) steps |> incrementLowest

let hm n = (((*) n) >> (+) )


let rec intersect max lcm n =
    // if  Array.map2 (fun i s -> s + n * loopSteps[i]) |> Array.forall ((%) lcm >> (=) 0L)
    if  Array.map2 (((*) n) >> (+) ) loopSteps initSteps |> Array.forall (fun s -> s % lcm = 0L)
    then n * max
    else intersect max lcm (n + 1L)

let part2 () =  intersect 19512L 6009696L 1L |> printfn "%A"
// let part2 () =  loopSteps |> printfn "%A"

// 21409; 16531; 19241; 19783; 14363; 15989
// 19512; 2439; 2981; 1897; 11924; 8672
// 6009696
// 19512 2439 2981 1897 11924 8672 21409 16531 19241 19783 14363 15989
// 469373449998698816
// 153934641126
// 9223372036854775807
