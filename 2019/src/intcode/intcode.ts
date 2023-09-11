export class Runtime {
    program: number[];
    originalProgram: number[];
    inputs: number[] = [];
    outputs: number[] = [];
    pointer: number = 0;
    realtiveBase: number = 0;
    halted: boolean = false;

    constructor(program: number[]) {
        this.originalProgram = program;
        this.program = [...this.originalProgram];
    };

    io(...inputs: number[]): number[] {
        this.inputs = inputs.reverse();
        this.outputs = [];
        this.program = [...this.originalProgram];
        this.pointer = 0;
        this.run();
        return this.outputs;
    }

    ioContinue(...inputs: number[]): number[] {
        this.inputs = inputs.reverse();
        this.run();
        return this.outputs;
    }

    run() {
        while (this.execute());
    }

    getValue(mode: string): number {
        return mode === '1' 
            ? this.program[this.pointer++] 
            : this.program[this.program[this.pointer++]];
    }
    
    execute(): boolean {
        const modes = ('' + this.program[this.pointer++]).padStart(5, '0');
        const instruction = modes.slice(3);
        switch (instruction) {
            case '01': // add
                const sum = this.getValue(modes[2]) + this.getValue(modes[1]);
                this.program[this.program[this.pointer++]] = sum;
                return true;
            case '02': // multiply
                const product = this.getValue(modes[2]) * this.getValue(modes[1]);
                this.program[this.program[this.pointer++]] = product;
                return true;
            case '03': // input
                if(!this.inputs.length) {
                    this.pointer--;
                    return false;
                }
                this.program[this.program[this.pointer++]] = this.inputs.pop() ?? 0;
                return true;
            case '04': // output
                this.outputs.push(this.getValue(modes[2]));
                return true;
            case '05': // jump-if-true
                const isTrue = this.getValue(modes[2]) != 0;
                this.pointer = isTrue ? this.getValue(modes[1]) : this.pointer + 1;
                return true;
            case '06': // jump-if-false
                const isFalse = this.getValue(modes[2]) == 0;
                this.pointer = isFalse ? this.getValue(modes[1]) : this.pointer + 1;
                return true;
            case '07': // less than
                const lt = this.getValue(modes[2]) < this.getValue(modes[1]);
                this.program[this.program[this.pointer++]] = lt ? 1 : 0;
                return true;
            case '08': // equals
                const eq = this.getValue(modes[2]) == this.getValue(modes[1]);
                this.program[this.program[this.pointer++]] = eq ? 1 : 0;
                return true;
            case '99':
                this.halted = true;
                return false;
            default:
                throw new Error(`Halted on instruction '${instruction}'`);
        }
    }
}
