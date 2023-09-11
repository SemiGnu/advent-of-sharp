import fs from 'fs';

function getData(test: boolean): number[] {
    const file = __filename.replace('day.ts', `${test ? 'test' : 'data'}.txt`);
    return fs.readFileSync(file, 'utf8').split('').map(n => parseInt(n));
}

function chunk<T>(source: T[], size: number): T[][] {
    const chunks: T[][] = [];
    for (let i = 0; i < source.length; i += size) {
        chunks.push(source.slice(i, i+size));
    }
    return chunks;
}

export function part1(test: boolean): string {
    const data = getData(test);
    const chunks = chunk(data, test ? 6 : 150);

    const fewest0s = chunks.slice(1).reduce((a,c) => {
        let count = c.filter(n => n === 0).length;
        if (count >= a.count) return a;
        return { count, c }
    }, {count: chunks[0].filter(n => n === 0).length, c: chunks[0]}).c;

    const ones = fewest0s.filter(n => n === 1).length;
    const twos = fewest0s.filter(n => n === 2).length;

    console.log(fewest0s);
    return ''+ ones * twos;
}



export function part2(test: boolean): string {
    const data = getData(test);
    const w = test ? 2 : 25;
    const h = test ? 2 : 6;
    const size = w * h;

    const chunks = chunk(data, size);
    const final = chunks.reduce((acc, cur) => {
        for(let i = 0; i < acc.length; i++) {
            if (acc[i] === 2) {
                acc[i] = cur[i];
            }
        }
        return acc;
    }, [...Array(size)].map((_) => 2));

    let render = '';
    for(let i = 0; i < final.length; i++) {
        render += final[i] === 0 ? ' ' : 'X';
        if (i % w == w - 1) {
            render += '\n';
        }
    }

    return '' + render;
}