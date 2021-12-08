module _06

// PARSING 

let lines = System.IO.File.ReadLines("06/data.txt") 

let fish = (Seq.head lines).Split "," |> Array.map int |> Array.toSeq

// PART 1

let fishAges = fish |> (Seq.groupBy id)

let getAge (i:int) = if Seq.exists (fun age -> fst age = i) fishAges then Seq.find (fun age -> fst age = i) fishAges |> (int64 << Seq.length << snd) else 0L

let startAges = Array.init 9 getAge

let rec generation i a (ages:int64[]) = 
    match i with 
    | 0 -> generation (i+1) ages[i] <| Array.tail ages
    | 7 -> (ages[0] + a) :: (generation (i+1) a <| Array.tail ages)
    | 9 -> [a]
    | _ -> ages[0] :: (generation (i+1) a <| Array.tail ages)

let rec numEndFish n (ages:int64[]) = 
    match n with
    | 0 -> ages
    | _ -> numEndFish (n-1) <| List.toArray (generation 0 0 ages)

let part1 = sprintf "%d" <| (Array.sum <| numEndFish 80 startAges)

//PART 2

let part2 = sprintf "%d" <| (Array.sum <| numEndFish 256 startAges)