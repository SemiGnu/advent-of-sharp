import fs from 'fs';
import { type } from 'os';

function getData(test: boolean): string[] {
    const file = __filename.replace('day.ts', `${test ? 'test' : 'data'}.txt`);
    return fs.readFileSync(file, 'utf8').split('\n');
}

type Roid = {x: number, y: number};
type Roids = Roid[]

function getRoids(test: boolean): Roid[] {
    const lines = getData(test);
    const roids: Roid[] = [];
    for(let l = 0; l < lines.length; l++) {
        for(let c = 0; c < lines[l].length; c++) {
            if (lines[l][c] === '#') {
                roids.push({x: c, y: l});
            }
        }
    }
    return roids;
}

function canDetect(roid: Roid, probe: Roid, roids: Roids): boolean {
    let startIndex = roids.indexOf(roid);
    let probeIndex = roids.indexOf(probe);
    if (probeIndex === startIndex) return false;
    let relX = probe.x - roid.x;
    let relY = probe.y - roid.y;
    let isBlocked = roids.some(potentialBlocker => {
        let blockerIndex = roids.indexOf(potentialBlocker);
        if (blockerIndex == startIndex || blockerIndex == probeIndex) return false;
        let blockerRelX = potentialBlocker.x - roid.x;
        let blockerRelY = potentialBlocker.y - roid.y;
        let result = false;
        if(!((relX > 0 ? blockerRelX > 0 : blockerRelX < 0 && relX < 0) || (relY > 0 ? blockerRelY > 0 : blockerRelY < 0 && relY < 0))) {
            result = false;
        }
        else if (Math.abs(blockerRelX) > Math.abs(relX) || Math.abs(blockerRelY) > Math.abs(relY)) {
        } else if ((relX === 0 && blockerRelX === 0) || (relY === 0 && blockerRelY === 0)) {
            result = true;
        } else if(blockerRelX / relX == blockerRelY / relY) {
            result = true;
        } else {
        }
        if(roid.x === 4 && roid.y === 0 && probe.y === 3 && potentialBlocker.y === 2) {
        }
        return result;
    })
    return !isBlocked;
}
 
function countRoids(roid: Roid, roids: Roids): number {
    return roids.filter(probe => canDetect(roid, probe, roids)).length;
} 
function detectableRoids(roid: Roid, roids: Roids): Roids {
    return roids.filter(probe => canDetect(roid, probe, roids));
}

export function part1(test: boolean): string {
    const roids = getRoids(test);
    let bestRoid = roids.map((roid) => {return {roid, count: countRoids(roid, roids)}}).sort((a,b) => b.count - a.count)[0]
    return '' + bestRoid.count;
}

export function part2(test: boolean): string {
    const roids = getRoids(test);
    let bestRoid = roids.map((roid) => {return {r: roid, c: countRoids(roid, roids)}}).sort((a,b) => b.c - a.c)[0].r;
    let seenRoids = detectableRoids(bestRoid, roids).map(roid => {
        let angle = Math.PI / 2 - Math.atan2(- roid.y + bestRoid.y, roid.x - bestRoid.x);
        if (angle < 0 ) angle += Math.PI*2;
        return { roid, angle };
    });
    let destroyOrder = seenRoids.sort((a,b) => a.angle - b.angle);
    let _200thRoid = destroyOrder[199].roid;
    return '' + (_200thRoid.x * 100 + _200thRoid.y);
}