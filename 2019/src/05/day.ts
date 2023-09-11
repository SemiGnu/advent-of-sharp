import fs from 'fs';
import { Runtime } from '../intcode/intcode'

function getData(test: boolean): string[] {
    const file = test ? './src/05/test.txt' : './src/05/data.txt';
    return fs.readFileSync(file, 'utf8').split(',');
}

export function part1(test: boolean): string {
    let runtime = new Runtime(getData(test).map(s => parseInt(s)));
    return '' + runtime.io(1);
}

export function part2(test: boolean): string {
    let runtime = new Runtime(getData(test).map(s => parseInt(s)));
    return '' + runtime.io(5);
}
