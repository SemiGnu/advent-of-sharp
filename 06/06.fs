module _06

// PARSING 

let lines = System.IO.File.ReadLines("06/test2.txt") 

let fish = (Seq.head lines).Split "," |> Array.map int |> Array.toSeq
// PART 1

let rec endFish n (fish:seq<int>) = 
    match n with 
    | 0 -> fish
    | _ -> endFish (n - 1) <| Seq.fold (fun fs f -> if f = 0 then 8::6::fs else (f - 1)::fs) [] fish

let numEndFish n = Seq.length <| endFish n fish

let part1 = sprintf "%d" <| numEndFish 80

// PART 2

//let rec getFish age n =
//    match age with 
//    | 0 when n > 0 -> 1 + (pown 2 (n/7)) + getFish 8 n
//    | 0 -> 0
//    | _ -> getFish (age-1) (n-1) 
//let rec getFish age n =
//    match n with 
//    | 0 -> 1
//    | nn when age = 0 -> (pown 2 ((nn)/7)-1) + getFish 8 (nn-1) 
//    | nn -> getFish (age-1) (nn-1) 
    
//let numEndFish2 n = getFish 3 n
        
//let test1  = {1..30} |> Seq.toList |> List.map numEndFish
//let test2  = {1..30} |> Seq.toList |> List.map numEndFish2

//let hm = List.zip test1 test2

let ageFish = Seq.map ((-) 1)
let resetFish = Seq.filter ((>=) 0) 
let numNewFish = Seq.length << ( Seq.filter ((<) 0) )
let newFish fish = Seq.append (resetFish ( ageFish fish ) )  ( Seq.concat <| Seq.init (numNewFish fish) (fun _ -> (List.toSeq [6;8])) )

let rec endFish2 n fish = 
    match n with 
    | 0 -> fish
    | _ -> endFish2 (n-1) <| newFish fish

     
let numEndFish2 n = Seq.length <| endFish n fish       
    
let part2 = sprintf "%d" <| numEndFish2 200