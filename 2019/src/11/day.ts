import fs from 'fs';
import { Runtime } from '../intcode/intcode';

function getData(test: boolean): string[] {
    const file = __filename.replace('day.ts', `${test ? 'test' : 'data'}.txt`);
    return fs.readFileSync(file, 'utf8').split(',');
}

type Pos = {x: number, y: number, dir: number};

function advance(pos: Pos, turn: number): Pos {
    let dir = (pos.dir + (turn === 1 ? 1 : -1) + 4) % 4;
    pos.dir = dir;
    switch (pos.dir) {
        case 0: 
            pos.y--;
            break;
        case 1: 
            pos.x++;
            break;
        case 2: 
            pos.y++;
            break;
        case 3: 
            pos.x--;
            break;
        default:
            throw new Error('f');
    }
    if (pos.y > 100 || pos.x > 100 || pos.y < 0 || pos.x < 0) throw new Error('bounds');
    return pos;
}

function draw(tiles: number[][], pos: Pos) {
    const s = tiles.map(c => c.reduce((aa,cc) => aa + (cc ? '#' : '.'), ''));
    s[pos.y] = s[pos.y].slice(0, pos.x) + '' + pos.dir + s[pos.y].slice(pos.x + 1) ;
    console.log(s);
}

export function part1(test: boolean): string {
    const program = getData(false).map(n => parseInt(n));
    const tiles = [...Array(101)].map(_ => [...Array(101)].map(_ => 0));
    const visitedTiles = new Set<string>();
    let pos = {x: 50, y:50, dir: 0};
    const rt = new Runtime(program);
    while (!rt.halted) {
        let [color, turn] = rt.ioContinue(tiles[pos.y][pos.x]).slice(-2);
        visitedTiles.add(`${pos.x},${pos.y}`);
        tiles[pos.y][pos.x] = color;
        pos = advance(pos, turn);
    }
    draw(tiles, pos);
    return '' + visitedTiles.size;
}


export function part2(test: boolean): string {
    const program = getData(false).map(n => parseInt(n));
    const tiles = [...Array(101)].map(_ => [...Array(101)].map(_ => 0));
    tiles[50][50] = 1;
    const visitedTiles = new Set<string>();
    let pos = {x: 50, y:50, dir: 0};
    const rt = new Runtime(program);
    while (!rt.halted) {
        let [color, turn] = rt.ioContinue(tiles[pos.y][pos.x]).slice(-2);
        visitedTiles.add(`${pos.x},${pos.y}`);
        tiles[pos.y][pos.x] = color;
        pos = advance(pos, turn);
    }
    draw(tiles, pos);
    return '' + visitedTiles.size;
}