module AoC2023.Day04

let lines = System.IO.File.ReadLines "04/data"

let parseCard (line:string) = (line.Split(":")[1]).Split("|")
                              |> Seq.map (fun (s:string) ->
                                  s.Split(" ", System.StringSplitOptions.RemoveEmptyEntries) |> Seq.map int |> Set.ofSeq)
let scoreCard = Set.intersectMany >> Set.count >> fun n -> pown 2 (n - 1)
let part1 () = lines |> Seq.sumBy (parseCard >> scoreCard) |> printfn "%i"

let superSum big small =  let l = Seq.length small in
                          List.map2 (+) (List.take l big) small @ List.skip l big
let nms n m = List.init n (fun _ -> m)
let rec counter l m =
    match l with
    | [] -> 0
    | n::ns -> n + counter (superSum ns (nms (Seq.head m) n)) (List.tail m) 
let part2 () = lines |> Seq.map (parseCard >> Set.intersectMany >> Set.count) |> List.ofSeq |> counter (nms (Seq.length lines) 1) |>  printfn "%A"
