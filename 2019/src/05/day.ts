import fs from 'fs';
import { execute } from '../intcode/intcode'

function getData(test: boolean): string[] {
    const file = test ? './src/04/test.txt' : './src/04/data.txt';
    return fs.readFileSync(file, 'utf8').split(',');
}


export function part1(test: boolean): string {
    execute([]);
    return '';
}


export function part2(test: boolean): string {
    return '';
}