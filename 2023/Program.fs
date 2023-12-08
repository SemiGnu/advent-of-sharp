module AoC2023.Main

open AoC2023.Day08
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

[<MemoryDiagnoser>]
type PartTester () =

    [<Benchmark>]
    member self.part2 () = part2 ()


[<EntryPoint>]
let Main args = BenchmarkRunner.Run typeof<PartTester> |> (fun _ -> 0)
