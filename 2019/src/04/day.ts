import fs from 'fs';

function getData(test: boolean): string[] {
    const file = test ? './src/04/test.txt' : './src/04/data.txt';
    return fs.readFileSync(file, 'utf8').split('-');
}

function isValid(n: number): boolean {
    const s = '' + n;
    let consec = 0;
    let dec = false;
    for(let i = 0; i < s.length - 1; i++) {
        if (s[i] == s[i+1]) {
            consec ++;
        }
        if (s[i] > s[i+1]) {
            dec = true;
            break;
        }
    }
    return !dec && !!consec
}

function isMoreValid(n: number): boolean {
    const s = ('' + n).split('');
    const hist = [...Array(10)].map(_ => 0);
    for(let c of s) {
        hist[parseInt(c)]++;
    }
    return hist.some(x => x === 2) && isValid(n);
}

function findValids(min: number, max: number, validator: (n: number) => boolean): number {
    let number = min;
    let valids = 0;
    while (number <= max) {
        if(validator(number)) valids++;
        number++;
    }
    return valids;
}

export function part1(test: boolean): string {
    const bounds = getData(test);
    let min = parseInt(bounds[0]);
    let max = parseInt(bounds[1])
    return '' + findValids(min, max, isValid);
}


export function part2(test: boolean): string {
    const bounds = getData(test);
    let min = parseInt(bounds[0]);
    let max = parseInt(bounds[1])
    return '' + findValids(min, max, isMoreValid);
}