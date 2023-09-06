import fs from 'fs';

function getPaths(test: boolean): string[][] {
    const file = test ? './src/03/test.txt' : './src/03/data.txt';
    return fs.readFileSync(file, 'utf8').split('\n').map(l => l.split(','));
}

function explode(path: string, start: number[]) : number[][] {
    const v = path.startsWith('U') ? 1 : path.startsWith('D') ? -1 : 0;
    const h = path.startsWith('R') ? 1 : path.startsWith('L') ? -1 : 0;
    const l = parseInt(path.slice(1));
    const range = [...Array(l)].map((_,i) => i + 1);
    return range.map(i => [start[0] + v * (i), start[1] + h * (i)]);
}

function getPath(path: string[], start: number[]) : number [][] {
    if (!path.length) {
        return [];
    }
    const nextBit = explode(path[0], start);
    return [...nextBit, ...getPath(path.slice(1), nextBit[nextBit.length - 1])]
}

export function part1(test: boolean): string {
    let paths = getPaths(test).map(p => getPath(p, [0,0]));
    const xings: number[] = []
    for (let e0 of paths[0]) {
        for (let e1 of paths[1]) {
            if(e0[0] == e1[0] && e0[1] == e1[1]) {
                xings.push(Math.abs(e0[0]) + Math.abs(e1[1]));
            }
        }
    }
    
    return '' + Math.min(...xings);
}


export function part2(test: boolean): string {
    let paths = getPaths(test).map(p => getPath(p, [0,0]));
    const xings: number[] = []
    for (let i = 0; i < paths[0].length; i++) {
        let e0 = paths[0][i];
        for (let j = 0; j < paths[1].length; j++) {
            let e1 = paths[1][j];
            if(e0[0] == e1[0] && e0[1] == e1[1]) {
                xings.push(i+j+2);
            }
        }
    }
    
    return '' + Math.min(...xings);
}