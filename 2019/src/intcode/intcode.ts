

type PositionalParameter = {
    value: number;
}

type ImmediateParameter = {
    value: number;
}

type Parameter = PositionalParameter | ImmediateParameter;

type Multiply = {
    length: 4;
    first: Parameter;
    second: Parameter;
    location: ImmediateParameter;
}
type Add = {
    length: 4;
    first: Parameter;
    second: Parameter;
    location: ImmediateParameter;
}
type Input = {
    length: 2;
    parameter: Parameter
}
type Output = {
    length: 2;
    parameter: Parameter
}
type Halt = {
    length: 1;
}

type Instruction = Multiply | Add | Input | Output | Halt

export function execute(program: string[]) {
    let test: Instruction = { length: 2, parameter: {value: 3 }};
    
    console.log(test.parameter)
    
}


