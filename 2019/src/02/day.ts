import fs from 'fs';


function getInstructions(test: boolean): string[] {
    const file = test ? './src/02/test.txt' : './src/02/data.txt';
    return fs.readFileSync(file, 'utf8').split(',');
}


function execute(program: number[], noun: number, verb: number ) : number {
    program[1] = noun;
    program[2] = verb;

    let pointer = -1;
    let halt = false;
    while (!halt) {
        switch (program[++pointer]) {
            case 1:
                const sum = program[program[++pointer]] + program[program[++pointer]]
                program[program[++pointer]] = sum;
                break;
            case 2:
                const prod = program[program[++pointer]] * program[program[++pointer]]
                program[program[++pointer]] = prod;
                break;
            default:
                halt = true;
                break;
        }
    }
    return program[0]
}


export function part1(test: boolean): string {
    const program = getInstructions(test).map(Number);
    return "" + execute(program, 12, 2);
}

export function part2(test: boolean): string {
    const program = getInstructions(test).map(Number);
    const target = 19690720;
    let result = 0;
    for (let i = 0; i < 100; i++) {
        for (let j = 0; j < 100; j++) {
            result = execute([...program], i, j )
            if(result == target) {
                return "" + (100*i + j)
            }
        }
    }
    return "";
}