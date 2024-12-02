module AoC2024.Main

open AoC2024.Day01
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

[<MemoryDiagnoser>]
type PartTester () =

    [<Benchmark>]
    member self.part2 () = part1 ()


[<EntryPoint>]
let Main args = BenchmarkRunner.Run typeof<PartTester> |> (fun _ -> 0)
