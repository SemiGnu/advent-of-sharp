module _09

// PARSING

let lines = System.IO.File.ReadLines("09/test.txt") |> Seq.toArray

let grid = lines |> Array.map (fun line -> line |> Seq.map (fun c -> int c - 48) |> Seq.toArray)

// PART 1

let maxY = grid.Length - 1
let maxX = grid[0].Length - 1

let get (grid:int[][]) coords = 
    match coords with
    | Some(x,y) -> if x >= 0 && y >= 0 || x < grid[0].Length || y < grid.Length then grid[y][x] else 9
    | None -> 9

let minimum (grid:int[][]) coords n = if n < get grid coords && n < get grid coords && n < get grid coords && n < get grid coords then coords else None
    //let up = if y = 0 then true else n < grid[y-1][x]
    //let down = if y = maxY then true else n < grid[y+1][x]
    //let left = if x = 0 then true else n < grid[y][x-1]
    //let right = if x = maxX then true else n < grid[y][x+1]
    //if up && down && left && right then n + 1 else 0

let a = grid |> Array.mapi (fun y row -> row |> Array.mapi (fun x n -> minimum grid (Some(x,y)) n) |> Array.filter Option.isSome)  

let minima = grid |> Array.mapi (fun y row -> row |> Array.mapi (fun x n -> minimum grid (Some(x,y)) n) |> Array.filter Option.isSome |> Array.length)  |> Array.sum

let part1 = sprintf "%d" <| minima

// PART 2
let neighbours (grid:int[][]) coords  =
    match coords with
    | None -> [||]
    | Some(x, y) ->
        let up = if y = 0 && grid[y-1][x] < 9 then Some(x, y-1) else None
        let down = if y = maxY && grid[y+1][x] < 9 then Some(x, y-1) else None
        let left = if x = 0 && grid[y][x-1] < 9 then Some(x, y-1) else None
        let right = if x = maxX && grid[y][x+1] < 9 then Some(x, y-1) else None
        [| up ; down ; left ; right |] |> Array.filter Option.isSome




//let minima2 = grid |> Array.mapi (fun y row -> row |> Array.mapi (fun x n -> compare2 grid x y n) |> Array.filter Option.isSome |> Array.map Option.) |> Array.concat


let part2 = sprintf "%d" <| 0
