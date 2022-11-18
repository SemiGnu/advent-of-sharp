module _11

// PARSING

let lines = System.IO.File.ReadLines("11/test.txt") |> Seq.toArray

let grid = lines |> Array.map (fun line -> line |> Seq.map (fun c -> int c - 48) |> Seq.toArray)

// PART 1

let editGrid pred f = Array.iteri (fun y row -> row |> Array.iteri (fun x n -> if pred x y n then do row[x] <- f n))

let getAdjacent x y = Array.allPairs [|(max x 0)..(min x 9)|] [|(max y 0)..(min y 9)|]

let getFlashes = Array.mapi (fun y row -> row |> Array.mapi (fun x n -> if n > 9 then getAdjacent x y else [||]) |> Array.concat ) >> Array.concat >> Array.distinct

let rec subStep flashes grid = 
    let newFlashes = Array.except flashes <| getFlashes grid
    if newFlashes.Length = 0 
    then
        do editGrid (fun _ _ n -> n < 0) (fun _ -> 0) grid
        0
    else
        do editGrid (fun x y _ -> Array.contains (x, y) newFlashes) (fun n -> n + 1) grid
        newFlashes.Length + subStep (Array.append flashes newFlashes) grid

let step grid = 
    editGrid (fun _ _ n -> n > 9) (fun n -> 0) grid //reset
    editGrid (fun _ _ _ -> true) (fun n -> n + 1) grid //increment
    subStep [||] grid

let octopus n grid = [|0..n|] |> Array.map (fun _ -> step grid)

let test = octopus 10 grid

let part1 = sprintf "%d" <| 0

// PART 2

let part2 = sprintf "%d" <| 0
