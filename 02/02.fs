module _02

open System.Text.RegularExpressions

// PARSING

let lines = System.IO.File.ReadLines("02/data.txt")

type command = { direction: string; magnitude: int }

let parseLine line = 
    let g = Regex("(forward|up|down) (\d*)").Match(line).Groups
    in { direction = g[1].Value; magnitude = int g[2].Value }

let commands = lines |> Seq.map parseLine |> Seq.toList

// PART 1

type state = { depth: int; position: int }

let depthChange command = if command.direction = "forward" then 0 else if command.direction = "up" then -command.magnitude else command.magnitude
let positionChange command = if command.direction = "forward" then command.magnitude else 0

let finalState = commands |> Seq.fold (fun state command -> { depth = state.depth + depthChange command; position = state.position + positionChange command}) {depth= 0; position = 0}

let value state = state.depth * state.position

let part1 = sprintf "%d" <| value finalState

// PART 2

type state2 = { depth: int; position: int; aim: int }

let aimChange command = if command.direction = "forward" then 0 else if command.direction = "up" then -command.magnitude else command.magnitude
let depthChange2 command aim = if command.direction = "forward" then command.magnitude * aim else 0

let finalState2 = commands |> Seq.fold (fun state command -> { depth = state.depth + depthChange2 command state.aim; position = state.position + positionChange command; aim = state.aim + aimChange command}) {depth= 0; position = 0; aim = 0}

let value2 state = state.depth * state.position

let part2 = sprintf "%d" <| value2 finalState2
