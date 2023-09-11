export class Runtime {
    program: number[];
    originalProgram: number[];
    inputs: number[] = [];
    outputs: number[] = [];
    pointer: number = 0;
    realtiveBase: number = 0;
    operations: number = 0;
    halted: boolean = false;

    constructor(program: number[]) {
        this.originalProgram = program;
        this.program = [...this.originalProgram];
        if (this.program.some(n => n > 9007199254740991)) throw new Error("FUCK");
    };

    io(...inputs: number[]): number[] {
        this.inputs = inputs.reverse();
        this.outputs = [];
        this.program = [...this.originalProgram];
        this.pointer = 0;
        this.halted = false;
        this.realtiveBase = 0;
        this.operations = 0;
        this.run();
        return this.outputs;
    }

    ioContinue(...inputs: number[]): number[] {
        this.inputs = inputs.reverse();
        this.run();
        return this.outputs;
    }

    run() {
        let i = 0;
        while (this.execute());
    }

    getValue(mode: string): number {
        let value = 0;
        switch (mode) {
            case '0':
                value = this.program[this.program[this.pointer++]] ?? 0;
                break;
            case '1':
                value = this.program[this.pointer++] ?? 0;
                break;
            case '2':
                value = this.program[this.realtiveBase + this.program[this.pointer++]] ?? 0;
                break;
            default:
                throw new Error(`Halted on mode '${mode}'`);
        }
        console.log(value)
        return value;
    }

    setValue(value: number) {
        if (value > 9007199254740991) throw new Error("FUCK");
        console.log(`Storing ${value} at:`)
        const location = this.getValue('1')
        if (this.program.length <= location) {
            this.program = [...this.program, ...[...Array(location - this.program.length + 1)].map(_ => 0)]
        }
        this.program[location] = value;
    }
    
    execute(): boolean {
        const modes = ('' + this.program[this.pointer++]).padStart(5, '0');
        const instruction = modes.slice(3);
        this.operations++;
        switch (instruction) {
            case '01': // add
                console.log("add")
                const sum = this.getValue(modes[2]) + this.getValue(modes[1]);
                this.setValue(sum);
                return true;
            case '02': // multiply
                console.log("multiply")
                const product = this.getValue(modes[2]) * this.getValue(modes[1]);
                this.setValue(product);
                return true;
            case '03': // input
                if(!this.inputs.length) {
                    this.pointer--;
                    return false;
                }
                console.log("input")
                this.setValue(this.inputs.pop() ?? 0);
                return true;
            case '04': // output
                console.log("output")
                this.outputs.push(this.getValue(modes[2]));
                return true;
            case '05': // jump-if-true
                console.log("jump-if-true")
                const isTrue = this.getValue(modes[2]) != 0;
                this.pointer = isTrue ? this.getValue(modes[1]) : this.pointer + 1;
                return true;
            case '06': // jump-if-false
                console.log("jump-if-false")
                const isFalse = this.getValue(modes[2]) == 0;
                this.pointer = isFalse ? this.getValue(modes[1]) : this.pointer + 1;
                return true;
            case '07': // less than
                console.log("less than")
                const lt = this.getValue(modes[2]) < this.getValue(modes[1]);
                this.setValue(lt ? 1 : 0);
                return true;
            case '08': // equals
                console.log("equals")
                const eq = this.getValue(modes[2]) == this.getValue(modes[1]);
                this.setValue(eq ? 1 : 0);
                return true;
            case '09': // adjust relavite base
                console.log("adjust relavite base")
                this.realtiveBase += this.getValue(modes[2]);
                return true;
            case '99': // halt
                this.halted = true;
                return false;
            default:
                throw new Error(`Halted on instruction '${instruction}'`);
        }
    }
}
