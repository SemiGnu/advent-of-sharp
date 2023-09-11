import fs from 'fs';
import { Runtime } from '../intcode/intcode';

function getData(test: boolean): string[] {
    const file = __filename.replace('day.ts', `${test ? 'test' : 'data'}.txt`);
    return fs.readFileSync(file, 'utf8').split(',');
}

function getPermutations(list: number [], prev: number[] = []): number [][] {
    if ( list.length == 1) return [[...prev, list[0]]];
    return [... list.flatMap(n => {
        let i = list.indexOf(n);
        let l = [...list.slice(0, i), ...list.slice(i + 1)];
        return [ ...getPermutations(l, [...prev, n])];
    })];
}

export function part1(test: boolean): string {
    let program = getData(test).map(d => parseInt(d));
    const runtime = new Runtime(program);
    const phaseSequencePermutations = getPermutations([0,1,2,3,4]);

    let maxOutput = 0;
    let bestPermutation = phaseSequencePermutations[0];

    for (let phaseSequence of phaseSequencePermutations) {
        let input = 0;
        phaseSequence.forEach(ps => {
            input = runtime.io(ps, input)[0];
        });
        if( input > maxOutput ) {
            maxOutput = input;
            bestPermutation = phaseSequence;
        } 
    }
    return `Max ouput: ${maxOutput}, phase sequence: ${bestPermutation}`;
}


export function part2(test: boolean): string {
    let program = getData(test).map(d => parseInt(d));
    const phaseSequencePermutations = getPermutations([5,6,7,8,9]);

    let maxOutput = 0;
    let bestPermutation = phaseSequencePermutations[0];
    for (let phaseSequence of phaseSequencePermutations) {
        let amplifiers = phaseSequence.map(ps => {
            const r = new Runtime(program);
            r.io(ps);
            return r;
        })
    
        let step = 0;
        let input = 0;
    
        while (amplifiers.some(a => !a.halted)) {
            let next = amplifiers[step % 5].ioContinue(input);
            input = next[next.length - 1];
            step++;
        }
        if( input > maxOutput ) {
            maxOutput = input;
            bestPermutation = phaseSequence;
        } 
    }


    return `Max ouput: ${maxOutput}, phase sequence: ${bestPermutation}`;
}