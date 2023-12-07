module AoC2023.Day07

open System

let lines = System.IO.File.ReadLines "07/data"
let parseHand (hand:string) = let a = hand.Split(' ')
                              (a[0].Replace('A', 'E').Replace('K', 'D').Replace('Q', 'C').Replace('J', 'B').Replace('T', 'A'), int a[1])
let hex s = Convert.ToInt32("0x" + s , 16);
let scoreHand (cards:string, bet:int) =
    match cards |> Seq.groupBy id |> Array.ofSeq with
    | g when g.Length = 1 -> ("7" + cards |> hex, bet) 
    | g when g.Length = 2 && g |> Array.exists (snd >> Seq.length >> (=) 4) -> ("6" + cards |> hex, bet) 
    | g when g.Length = 2 && g |> Array.exists (snd >> Seq.length >> (=) 3) -> ("5" + cards |> hex, bet) 
    | g when g.Length = 3 && g |> Array.exists (snd >> Seq.length >> (=) 3) -> ("4" + cards |> hex, bet) 
    | g when g.Length = 3 -> ("3" + cards |> hex, bet) 
    | g when g.Length = 4 -> ("2" + cards |> hex, bet) 
    | _ -> (cards |> hex, bet)
let winnings lines = lines |> Seq.map parseHand |> Seq.sortBy (scoreHand >> fst) |> Seq.mapi (fun i (_,bet) -> bet*(i+1)) |> Seq.sum
let part1 () = lines |> winnings |> printfn "%A"




let parseHand2 (hand:string) = let a = hand.Split(' ')
                               (a[0].Replace('A', 'E').Replace('K', 'D').Replace('Q', 'C').Replace('J', '1').Replace('T', 'A'), int a[1])
let nonJokers = seq {'2'; '3'; '4'; '5'; '6'; '7'; '8'; '9'; 'A'; 'B'; 'C'; 'D'; 'E'; }
let scoreHand2 (c,(cards:string, bet:int)) =
    match cards.Replace('1',c) |> Seq.groupBy id |> Array.ofSeq with
    | g when g.Length = 1 -> ("7" + cards |> hex, bet)
    | g when g.Length = 2 && g |> Array.exists (snd >> Seq.length >> (=) 4) -> ("6" + cards |> hex, bet)
    | g when g.Length = 2 && g |> Array.exists (snd >> Seq.length >> (=) 3) -> ("5" + cards |> hex, bet)
    | g when g.Length = 3 && g |> Array.exists (snd >> Seq.length >> (=) 3) -> ("4" + cards |> hex, bet)
    | g when g.Length = 3 -> ("3" + cards |> hex, bet)
    | g when g.Length = 4 -> ("2" + cards |> hex, bet)
    | _ -> (cards |> hex, bet)

let scoreHand4 hand= Seq.allPairs nonJokers [hand] |> Seq.map scoreHand2 |> Seq.max
let winnings2 lines = lines |> Seq.map parseHand2 |> Seq.sortBy (scoreHand4 >> fst) |> Seq.mapi (fun i (_,bet) -> bet*(i+1)) |> Seq.sum
let part2 () = lines |> winnings2 |> printfn "%A"

// 251037509
