module _04

open System

// PARSING 

let lines = Seq.toList <| System.IO.File.ReadLines("04/data.txt")

let bingoNumbers = (List.head lines).Split "," |> Array.map int |> Array.toList

let sets = List.skip 1 lines |> List.chunkBySize 6 |> List.map (List.skip 1) 

let bingoBoards = sets |> List.map (fun set -> set |> List.map (fun (line: string) -> List.map int (Array.toList <| line.Split(" ", StringSplitOptions.RemoveEmptyEntries)) ))

// PART 1

let checkOrthagonal row numbers = row |> List.map (fun x -> List.contains x numbers) |> List.contains false |> not

let checkRows board numbers = board |> List.map (fun row -> checkOrthagonal row numbers) |> List.contains true

let rec getColumnsRec (board:'a list list) index = 
    match index with 
    | i when i = board[0].Length -> []
    | i ->  (List.map (List.item index) board) :: getColumnsRec board (index + 1 )
let getColumns board = getColumnsRec board 0

let checkColumns board numbers = getColumns board |> List.map (fun row -> checkOrthagonal row numbers) |> List.contains true

let checkBoard board numbers = checkRows board numbers || checkColumns board numbers

let rec findBoard numbers index boards = 
    if (boards |> List.map (fun board -> checkBoard board numbers) |> List.contains true)
    then (boards |> List.find (fun board -> checkBoard board numbers), numbers)
    else findBoard (bingoNumbers[index]::numbers) (index + 1) boards

let firstWinner boards = findBoard (List.take 5 bingoNumbers) 5 boards
let (winningBoard, usedNumbers) = firstWinner bingoBoards

let unmarkedSum board usedNumbers = board |> List.fold (fun allNums row -> row @ allNums) [] |> List.except usedNumbers |> List.sum 
let score board usedNumbers = unmarkedSum board usedNumbers * List.head usedNumbers

let part1 = sprintf "%d" <| score winningBoard usedNumbers

// PART 2

let rec findLastBoard numbers index boards =
    let newNums = bingoNumbers[index]::numbers in
    match boards with 
    | board::[] when not <| checkBoard board numbers -> findLastBoard newNums (index + 1) <| board::[]
    | board::[] -> (board, numbers)
    | boards -> boards |> List.filter (fun board -> checkBoard board newNums |> not) |> findLastBoard newNums (index + 1)

let lastWinner boards = findLastBoard (List.take 5 bingoNumbers) 5 boards
let (lastWinningBoard, allUsedNumbers) = lastWinner bingoBoards

let part2 = sprintf "%d" <| score lastWinningBoard allUsedNumbers
