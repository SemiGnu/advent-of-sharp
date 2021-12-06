module _03

// PARSING

let lines = Seq.toList <| System.IO.File.ReadLines("03/data.txt")
let grid = List.map (fun (line: string) -> Seq.toList line |> List.map (fun (c: char) -> int c - 48)) lines 

// PART 1

let findBit op grid column  = (grid |> List.map (fun (l:int list) -> l[column])) |> (fun list -> if (op (List.sum list) ( 1 + List.length grid / 2)) then 1 else 0)
let findCommonBit = findBit (fun a b -> a >= b)
let findLeastCommonBit = findBit (fun a b -> a < b)

let rec getLine bit col (grid:int list list)  = 
    match col with
    | x when (x + 1) = (List.length grid[0]) -> bit grid x :: []
    | x -> bit grid x :: getLine bit (x + 1) grid

let gammaLine = getLine findCommonBit    
let epsilonLine = getLine findLeastCommonBit    

let lineToNum line = List.rev <| line |> List.fold (fun acc cur -> (fst acc + 1., (snd acc) + (float cur * (2. ** fst acc)))) (0.,0.) |> snd

let gamma = lineToNum <| gammaLine 0 grid
let epsilon = lineToNum <| epsilonLine 0 grid

let part1 = sprintf "%d" <| int (epsilon * gamma)

// PART 2

let filterGrid remainingGrid column bit = List.filter (fun (l:int list) -> l[column] = bit) remainingGrid

let rec findLine bitFinder column grid  =
    match grid with
    | line when List.length line = 1 -> line[0]
    | lines -> findLine bitFinder (column + 1) <| (filterGrid lines column <| bitFinder lines column)

let findOxyLine = findLine findCommonBit
let findCo2Line = findLine findLeastCommonBit

let printLine line = List.iter (fun d -> printf "%d" d) 

let oxy = lineToNum <| findOxyLine 0 grid
let co2 = lineToNum <| findCo2Line 0 grid

let part2 = sprintf "%d" <| int ( co2 * oxy )
