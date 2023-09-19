import fs from 'fs';
import { Runtime } from '../intcode/intcode';

function getData(test: boolean): string[] {
    const file = __filename.replace('day.ts', `${test ? 'test' : 'data'}.txt`);
    return fs.readFileSync(file, 'utf8').split(',');
}


export function part1(test: boolean): string {
    const program = getData(false).map(n => parseInt(n));
    const rt = new Runtime(program);
    const output = rt.io();
    const blockSet = new Set<string>();
    for (let i = 0; i < output.length; i += 3) {
        let [x, y, tile] = output.slice(i, i+3);
        let coord = x + ',' + y;
        if (tile === 2) blockSet.add(coord)
        else if(blockSet.has(coord) && tile !== 2) blockSet.delete(coord);
    } 
    return '' + blockSet.size;
}

export function part2(test: boolean): string {
    const program = getData(false).map(n => parseInt(n));
    program[0] = 2;
    const game = new Game(program);
    game.auto(0);
    return '' + game.score;
}

class Game {
    board: number[][] = [[0]];
    score: number = 0;
    ballX: number = 0;
    paddleX: number = 0;
    runtime: Runtime;

    constructor(program: number[]) {
        this.runtime = new Runtime(program);

    }

    auto(start: number) {
        this.draw(start);
        setTimeout(() => {
            let dir = Math.max(Math.min(this.ballX - this.paddleX, 1), -1);
            this.auto(dir);
        }, 1);
    }


    draw(input: number) {
        let output = this.runtime.ioContinue(input);
        for (let i = 0; i < output.length; i += 3) {
            let [x, y, tile] = output.slice(i, i+3);
            if (x === -1 && y === 0) {
                this.score = Math.max(tile, this.score);
                continue;
            }
            if (y >= this.board.length) {
                let newRows = [...Array(y - this.board.length + 1)].map(_ => this.board[0].map(_ => 0));
                this.board = [...this.board, ...newRows];
            }
            if (x >= this.board[0].length) {
                let newColumns = [...Array(x - this.board[0].length + 1)].map(_ => 0);
                this.board.forEach(row => row = [...row, ...newColumns])
            }
            this.board[y][x] = tile;
            if(tile === 3) this.paddleX = x;
            if(tile === 4) this.ballX = x;
        } 
        this.print();
    }

    print() {
        let p = '';
        for (let y of this.board) { 
            for (let x of y) {
                switch (x) {
                    case 1:
                        p += '|';
                        break;
                    case 2:
                        p += 'X';
                        break;
                    case 3:
                        p += '=';
                        break;
                    case 4:
                        p += 'o';
                        break;
                    default:
                        p += ' ';
                        break;
                }
            }
            p += '\n';
        }
        p += 'Score: ' + this.score;
        console.log(p)
    }
}