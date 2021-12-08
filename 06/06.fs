module _06

// PARSING 

let lines = System.IO.File.ReadLines("06/data.txt") 

let fish = (Seq.head lines).Split "," |> Array.map int |> Array.toList

// PART 1

let fishAges = fish |> (List.groupBy id)

let startAges = Array.create 9 0L

for age in fishAges do startAges[fst age] <- snd age |> List.length |> int64

let generation (ages:int64 list) = ages.Tail @ [ ages.Head ] |> List.mapi (fun i f -> if i = 6 then f + ages.Head else f )

let rec finalGeneration n (ages:int64 list) = 
    match n with
    | 0 -> ages
    | _ -> ages |> generation |> finalGeneration (n-1)

let numEndFish n = List.sum <| (startAges |> Array.toList |> finalGeneration n)

let part1 = sprintf "%d" <|  numEndFish 80

//PART 2

let part2 = sprintf "%d" <| numEndFish 256
