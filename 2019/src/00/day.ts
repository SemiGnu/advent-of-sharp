import fs from 'fs';

function getData(test: boolean): string[] {
    const file = __filename.replace('day.ts', `${test ? 'test' : 'data'}.txt`);
    return fs.readFileSync(file, 'utf8').split('\n');
}


export function part1(test: boolean): string {

    return '';
}


export function part2(test: boolean): string {

    return '';
}