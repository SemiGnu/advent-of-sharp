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

let part1 () = (instructions, map, "AAA")
               |||> traverse 0
               |> printfn "%A"

let startNodes = map |> Map.keys |> Seq.where (fun s -> s[2] = 'A') |> Seq.toArray
let rec spookyTraverse times step (instructions:string) map (loc:string) =
    match instructions[step % instructions.Length] with
    | _ when loc[2] = 'Z' && times = 0 -> step
    | 'L' when loc[2] = 'Z' -> spookyTraverse (times - 1) 0 instructions map (Map.find loc map |> fst)
    | 'R' when loc[2] = 'Z' -> spookyTraverse (times - 1) 0 instructions map (Map.find loc map |> snd)
    | 'L' -> spookyTraverse times (step + 1) instructions map (Map.find loc map |> fst)
    | 'R' -> spookyTraverse times (step + 1) instructions map (Map.find loc map |> snd)

let initSteps = startNodes |> Array.map (spookyTraverse 0 0 instructions map >> int64)
let loopSteps = startNodes |> Array.map (spookyTraverse 3 0 instructions map >> int64)
let intersect =
    let inc = 3756060L // loopSteps |> Array.max
    let start = initSteps[loopSteps |> Array.findIndex ((=) inc)]
    let rec subIntersect (start: int64) (inc: int64) =
        let current = start + inc
        if (initSteps, loopSteps) ||> Array.map2 (fun i s -> (current - i) % s = 0)  |> Array.forall id
        then current
        else subIntersect (start + inc) inc
    subIntersect start inc


let part2 () =  initSteps |> printfn "%A"


// 21409; 16531; 19241; 19783; 14363; 15989
// 19512; 2439; 2981; 1897; 11924; 8672
// 40921; 40921; 138481; 99457; ?; ?;
// 6009696
// 19512 2439 2981 1897 11924 8672 21409 16531 19241 19783 14363 15989
// 469373449998698816
// 153934641126
// 9223372036854775807
// 19783; 12737; 19241; 16531; 14363; 11653 =>  9177460370549
// 1897; 8130; 2981; 2439; 11924; 8943 => 3756060
// 7; 30; 11; 9; 44; 33 => 13860
// 50135L; 85907L; ?; 26287L; 26287L; ?
// 39170340 35306964 266680260 76373220 99535590 161510580
// 127199600735809152
// 127199600735809136
// 11795205600000
