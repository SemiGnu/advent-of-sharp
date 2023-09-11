import fs from 'fs';
import { Runtime } from '../intcode/intcode';


function getInstructions(test: boolean): string[] {
    const file = test ? './src/02/test.txt' : './src/02/data.txt';
    return fs.readFileSync(file, 'utf8').split(',');
}

function execute(program: number[], noun: number, verb: number): number {
    let runtime = new Runtime(program);
    runtime.program[1] = noun;
    runtime.program[2] = verb;
    runtime.run();
    return runtime.program[0];
}


export function part1(test: boolean): string {
    const program = getInstructions(test).map(Number);
    return "" + execute(program, 12, 2);
}

export function part2(test: boolean): string {
    const program = getInstructions(test).map(Number);
    const target = 19690720;
    let result = 0;
    for (let i = 0; i < 100; i++) {
        for (let j = 0; j < 100; j++) {
            result = execute([...program], i, j )
            if(result == target) {
                return "" + (100*i + j)
            }
        }
    }
    return "";
}