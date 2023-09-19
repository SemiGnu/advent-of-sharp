import fs from 'fs';
import { type } from 'os';

function getData(test: boolean): string[] {
    const file = __filename.replace('day.ts', `${test ? 'test' : 'data'}.txt`);
    return fs.readFileSync(file, 'utf8').split('\n');
}

type Component = { chemical: string, quantity: number };
type Components = Map<string, number>;
type Reaction = { input: Components, output: Component};
type Reactions = Map<string, Reaction>;

function parseReaction(line: string): Reaction  {
    const io = line.split(' => ');
    return {input: io[0].split(', ').reduce((d,c) => addComponent(d, parseChemical(c)), new Map<string, number>), output: parseChemical(io[1])}
}

function addComponent(components: Components, component: Component): Components {
    components.set(component.chemical, component.quantity);
    return components;
}

function parseChemical(s: string): Component {
    let groups = /(\d*) (\w*)/.exec(s)?.slice(-2) ?? [];
    return { chemical: groups[1], quantity: parseInt(groups[0])}
}

function add(component: Component, components: Components): Components {
    components.set(component.chemical, component.quantity + (components.get(component.chemical) ?? 0))
    return components;
}

function sub(component: Component, components: Components): Components {
    return add({chemical: component.chemical, quantity: -component.quantity}, components);;
}

function has(component: Component, components: Components): boolean {
    if ((components.get(component.chemical) ?? 0) < component.quantity) {
        return false;
    }
    add({chemical: component.chemical, quantity: -component.quantity}, components);
    return true;
}

// function tryGet (components: Component[], reaction: Reaction) {
//     // let foundComponents = reaction.input.map(i => [components.find(c => c.chemical === i.chemical), i.quantity]);
//     let foundComponents = reaction.input.filter(i => components.some)
//     if (!foundComponents.some(fc => fc[0] ))
// }

function getOreForFuel(reactions: Reactions, goal: Component = {chemical: 'FUEL', quantity: 1}, stock: Components = new Map<string,number>()): Components
{
    if(goal.chemical === 'ORE') return add(goal, stock);
    let reaction = reactions.get(goal.chemical) ?? { input: new Map<string,number>(), output: goal};
    let hasStock = reaction.input.size > 1;
    reaction.input.forEach((quantity, chemical) => hasStock &&= has({chemical,quantity}, stock));
    if (hasStock) {
        reaction.input.forEach((quantity, chemical) => sub({chemical,quantity}, stock));
        return add(goal, stock);
    }

    reaction.input.forEach((quantity, chemical) => {
        stock = getOreForFuel(reactions, {chemical, quantity}, stock)
    });

    return stock;
}


export function part1(test: boolean): string {
    let reactions = getData(test).map(parseReaction).reduce((d,r) => d.set(r.output.chemical, r), new Map<string, Reaction>());
    // console.log(reactions)
    let components = getOreForFuel(reactions);
    console.log(components);
    return '';
}


export function part2(test: boolean): string {

    return '';
}