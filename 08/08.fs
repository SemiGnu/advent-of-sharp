module _08

// PARSING

let lines = System.IO.File.ReadLines("08/test.txt") |> Seq.toArray

let displays = lines |> Array.map (fun line -> let l = line.Split " | " |> Array.map (fun display -> display.Split " " |> Seq.map Seq.sort) in (l[0], l[1]) ) 

// PART 1

let easyDigits = [|2;3;4;7|] 

//let easy = displays |> Array.map (fun output -> snd output |> Array.filter (fun digits -> Array.contains digits.Length easyDigits) |> Array.length) |> Array.sum

let part1 = sprintf "%d" <| 0 //easy

// PART 2


let getDigits (allDigits:seq<seq<char>>) =
    let d1 = allDigits |> Seq.find (fun line -> (Seq.length line) = 2) |> Seq.sort
    let d4 = allDigits |> Seq.find (fun line -> (Seq.length line) = 4) |> Seq.sort 
    let d7 = allDigits |> Seq.find (fun line -> (Seq.length line) = 3) |> Seq.sort 
    let d8 = allDigits |> Seq.find (fun line -> (Seq.length line) = 7) |> Seq.sort 
    let d6 = allDigits |> Seq.filter ((=) 6 << Seq.length) |> Seq.find (fun line -> Seq.length (Seq.except line d1) = 1) |> Seq.sort
    let d5 = allDigits |> Seq.filter ((=) 5 << Seq.length) |> Seq.find (fun line -> Seq.length (Seq.except d6 line) = 0) |> Seq.sort
    let d3 = allDigits |> Seq.filter ((=) 5 << Seq.length) |> Seq.filter ((<>) d5) |> Seq.find (fun line -> Seq.length (Seq.except d7 line) = 2) |> Seq.sort
    let d2 = allDigits |> Seq.filter ((=) 5 << Seq.length) |> Seq.filter ((<>) d5) |> Seq.find ((<>) d3) |> Seq.sort
    let d9 = allDigits |> Seq.filter ((=) 6 << Seq.length) |> Seq.find (fun line -> Seq.length (Seq.except d3 line) = 1) |> Seq.sort
    let d0 = allDigits |> Seq.filter ((=) 6 << Seq.length) |> Seq.find (fun line -> Seq.length (Seq.except d3 line) = 2) |> Seq.sort
    [|d0;d1;d2;d3;d4;d5;d6;d7;d8;d9|]


//let hm display = snd display |> Array.map (fun n -> Array.findIndex (fun d -> Seq.isEmpty <| Seq.except d n && Seq.isEmpty <| Seq.except n d) ( getDigits <| fst display))

//let hm2 = [|displays[7]|] |> Array.map hm

//let hm4 = hm2[7] |> Array.rev |> Array.mapi (fun i n -> n * pown 10 i ) |> Array.sum

//let hm3 = hm2 |> Array.map (fun h -> h |> Array.rev |> Array.mapi (fun i n -> n * pown 10 i ) |> Array.sum)


let getValue (digits:seq<seq<char>>) (numbers:seq<seq<char>>) = numbers |> Seq.map (fun n -> Seq.findIndex (fun d -> (Seq.compareWith Operators.compare d n) = 0) digits) |> Seq.rev |> Seq.mapi (fun i n -> n * pown 10 i ) |> Seq.sum

let total = displays |> Array.map (fun inOut -> getValue (getDigits  (fst inOut)) (snd inOut)) |> Array.sum

let part2 = sprintf "%d" <| total
