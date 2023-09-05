import fs from 'fs';


function getLines(test: boolean): string[] {
    const file = test ? './src/01/test.txt' : './src/01/data.txt';
    return fs.readFileSync(file, 'utf8').split('\n');
}

function calcFuel(mass: number): number {
    if (mass < 9){
        return 0;
    }
    const fuel = Math.floor(mass / 3) - 2;
    return fuel + calcFuel(fuel);
}

export function part1(test: boolean): string {
    const masses = getLines(test).map(Number);
    const fuels = masses.map(m => Math.floor(m / 3) - 2);
    const sumFuel = fuels.reduce((acc, cur) => acc + cur, 0);
    return "" + sumFuel;
}

export function part2(test: boolean): string {
    const masses = getLines(test).map(Number);
    const fuels = masses.map(calcFuel);
    const sumFuel = fuels.reduce((acc, cur) => acc + cur, 0);
    return "" + sumFuel;
}