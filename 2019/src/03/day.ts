import fs from 'fs';

type Point = {
    x: number;
    y: number;
}


function getPaths(test: boolean): string[][] {
    const file = test ? './src/03/test.txt' : './src/03/data.txt';
    return fs.readFileSync(file, 'utf8').split('\n').map(l => l.split(','));
}


function explode(path: string[], start: number[]) : number[][] {
    const head= path[0]
    const v = head[0] == 'U' ;


    return[[]];
}



export function part1(test: boolean): string {
    const paths = getPaths(test);

    return '';
}

export function part2(test: boolean): string {
    return '';
}