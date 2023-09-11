import fs from 'fs';
import { Runtime } from '../intcode/intcode';

function getData(test: boolean): string[] {
    const file = __filename.replace('day.ts', `${test ? 'test' : 'data'}.txt`);
    return fs.readFileSync(file, 'utf8').split(',');
}


export function part1(test: boolean): string {
    let program = getData(false).map(n => parseInt(n));
    // program = [109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99];
    // program = [1102,34915192,34915192,7,4,7,99,0];
    const rt = new Runtime(program);
    let output = rt.io(1);
    console.log(rt)
    return '' + output;
}


export function part2(test: boolean): string {

    return '';
}