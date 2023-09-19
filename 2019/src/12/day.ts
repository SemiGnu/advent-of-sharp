import fs from 'fs';

function getData(test: boolean): string[] {
    const file = __filename.replace('day.ts', `${test ? 'test' : 'data'}.txt`);
    return fs.readFileSync(file, 'utf8').split('\n');
}

class Moon {
    pos: [number, number, number];
    vel: [number, number, number] = [0,0,0];

    constructor(line: string) {
        let pos = /<x=(-?\d+), y=(-?\d+), z=(-?\d+)>/.exec(line)?.slice(-3).map(n => parseInt(n)) ?? [0,0,0];
        this.pos = [pos[0],pos[1],pos[2]];
    }

    applyGravity(moons: Moon[]) {
        for(let moon of moons) {
            this.vel[0] += Math.max(Math.min(moon.pos[0] - this.pos[0], 1), -1);
            this.vel[1] += Math.max(Math.min(moon.pos[1] - this.pos[1], 1), -1);
            this.vel[2] += Math.max(Math.min(moon.pos[2] - this.pos[2], 1), -1);
        }
    }

    updatePosition() {
        this.pos[0] += this.vel[0];
        this.pos[1] += this.vel[1];
        this.pos[2] += this.vel[2];
    }

    getEnergy(): number {
        return this.pos.reduce((a,b) => a + Math.abs(b), 0) *  this.vel.reduce((a,b) => a + Math.abs(b), 0);
    }
}

function upDateMoons(moons: Moon[]) {
    for(let i = 0; i < moons.length; i++) {
        moons[i].applyGravity(moons.slice(0,i));
        moons[i].applyGravity(moons.slice(i+1));
    }
    moons.forEach(m => m.updatePosition());
}

export function part1(test: boolean): string {
    const moons = getData(test).map(l => new Moon(l));
    console.log(moons);
    let [i, steps] = [0, 1000];
    while(i++ < steps) {
        for(let i = 0; i < moons.length; i++) {
            moons[i].applyGravity(moons.slice(0,i));
            moons[i].applyGravity(moons.slice(i+1));
        }
        moons.forEach(m => m.updatePosition());
    }
    return '' + moons.reduce((a,c) => a + c.getEnergy(), 0);
}

export function part2(test: boolean): string {
    const moons = getData(test).map(l => new Moon(l));
    let searches = [true, true, true];
    let inits = searches.map((_,i) => moons.map(m => m.pos[i]).join(':') + '|' + moons.map(m => m.vel[i]).join(':'));
    let counters = [0,0,0]
    while(searches.some(s => s)) {
        upDateMoons(moons);
        let states = searches.map((_,i) => moons.map(m => m.pos[i]).join(':') + '|' + moons.map(m => m.vel[i]).join(':'));
        searches.forEach((_, i) => {
            if (searches[i]) counters[i]++;
            if (inits[i] === states[i]) searches[i] = false;
        });
    }

    return '' + lcm(...counters);
}

function lcm(...ns: number[]): number {
    return ns.reduce((a,c) => a * c / gcd(a,c), 1)
}

function gcd(x: number, y: number): number {
    let min = Math.min(x,y);
    let max = Math.max(x,y);
    if ( min == 0 ) return max;
    return gcd(min, max % min);
}

