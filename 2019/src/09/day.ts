import fs from 'fs';
import { Runtime } from '../intcode/intcode';

function getData(test: boolean): string[] {
    const file = __filename.replace('day.ts', `${test ? 'test' : 'data'}.txt`);
    return fs.readFileSync(file, 'utf8').split(',');
}


export function part1(test: boolean): string {
    let program = getData(false).map(n => parseInt(n));
    const rt = new Runtime(program);
    let output = rt.io(1);
    return '' + output;
}


export function part2(test: boolean): string {
    let program = getData(false).map(n => parseInt(n));
    const rt = new Runtime(program);
    let output = rt.io(2);
    return '' + output;
}