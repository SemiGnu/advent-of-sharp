module AoC2023.Day06

let lines = System.IO.File.ReadLines "06/data" |> Array.ofSeq
let parseLines (lines:string array) = lines |> Array.map (fun s -> (s.Split ':' |> Seq.item 1).Split(' ', System.StringSplitOptions.RemoveEmptyEntries) |> Array.map int)
let races (l:int array array) = Array.zip l[0] l[1]

let winnable (t,d) c = (c * (t - c)) > d
let allWinnable pair = seq {1..1000} |> Seq.skipWhile (not << winnable pair) |> Seq.takeWhile (winnable pair) |> Seq.length
let part1 () = lines |> parseLines |> races |> Array.map allWinnable |> Array.fold (*) 1 |> printfn "%A"

let realRace (lines:string array) = lines |> Array.map (fun s ->
    (s.Split ':' |> Seq.item 1).Replace(" ","") |> int64) |> Array.pairwise |> Array.head
let winnableL (t:int64,d:int64) (c:int64) = (c * (t - c)) > d

let rec searchL f (low:int64) (high:int64) =
    let mid = (high + low) / 2L in
    let next = if high > low then mid + 1L else mid - 1L
    match mid with
    | _ when f mid && not (f next) -> mid
    | _ when f mid -> searchL f mid high
    | _ -> searchL f low mid

let part2 () = let t,d = lines |> realRace
               let min = searchL (winnableL (t,d)) (t / 2L) 0
               let max = searchL (winnableL (t,d)) (t / 2L) t
               printfn $"%A{max - min + 1L}"
