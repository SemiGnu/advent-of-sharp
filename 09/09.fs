module _09

// PARSING

let lines = System.IO.File.ReadLines("09/data.txt") |> Seq.toArray

let grid = lines |> Array.map (fun line -> line |> Seq.map (fun c -> int c - 48) |> Seq.toArray)

// PART 1

let maxY = grid.Length - 1
let maxX = grid[0].Length - 1

let compare (grid:int[][]) x y n =
    let up = if y = 0 then true else n < grid[y-1][x]
    let down = if y = maxY then true else n < grid[y+1][x]
    let left = if x = 0 then true else n < grid[y][x-1]
    let right = if x = maxX then true else n < grid[y][x+1]
    if up && down && left && right then n + 1 else 0

let minima = grid |> Array.mapi (fun y row -> row |> Array.mapi (fun x n -> compare grid x y n) |> Array.sum) |> Array.sum

let part1 = sprintf "%d" <| minima

// PART 2

let compare2 (grid:int[][]) x y n =
    let up = if y = 0 then true else n < grid[y-1][x]
    let down = if y = maxY then true else n < grid[y+1][x]
    let left = if x = 0 then true else n < grid[y][x-1]
    let right = if x = maxX then true else n < grid[y][x+1]
    if up && down && left && right then Some(x,y) else None


let part2 = sprintf "%d" <| 0
