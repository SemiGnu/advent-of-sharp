import fs from 'fs';

function getData(test: boolean): string[] {
    const file = __filename.replace('day.ts', `${test ? 'test' : 'data'}.txt`);
    return fs.readFileSync(file, 'utf8').split('\n');
}

type TreeNode = { key: string, count: number, children: TreeNode[] }

function insert(tree: TreeNode, parent: string, child: string): boolean {
    if (tree.key == parent) {
        tree.children.push({ key: child, count: 0, children: [] });
        return true;
    }
    tree.count++;
    return !!tree.children.find(c => insert(c, parent, child));
} 

function getTree(test: boolean): TreeNode {
    let data = getData(test)
        .map(x => x.split(')'));
    const root: TreeNode = { key: 'COM', count: 0, children: []};
    while (data.length) {
        let newData: string[][] = [];
        while (data.length) {
            let d = data.pop() ?? [];
            if (!insert(root, d[0], d[1])) {
                newData.push(d);
            }
        }
        data = newData;
    }
    return root;
}

function countDescendants(tree: TreeNode): number {
    return tree.children.reduce((a,c) => 
        a + countDescendants(c), tree.children.length);
}

function countDescendantsRec(tree: TreeNode): number {
    return tree.children.reduce((a,c) => 
        a + countDescendantsRec(c), countDescendants(tree));
}

function findPath(tree: TreeNode, key: string, path: string[]): string[] | undefined {
    if (tree.key === key) return [...path, key];
    if (!tree.children.length) return undefined;
    return tree.children.map(c => findPath(c, key, [...path, tree.key])).find(p => !!p);
}

function print(tree: TreeNode, indent = 0) {
    console.log(''.padStart(indent * 2, ' ') + tree.key + ': ' + tree.children.length);
    tree.children.forEach(c => print(c, indent + 1))
}

export function part1(test: boolean): string {
    const tree = getTree(test);
    return '' + countDescendantsRec(tree);
}


export function part2(test: boolean): string {
    const tree = getTree(test);
    const youPath = findPath(tree, 'YOU', []) ?? [];
    const sanPath = findPath(tree, 'SAN', []) ?? [];
    let i = 0;
    for (i; youPath[i] == sanPath[i]; i++) continue;
    i++;
    const distance = youPath.length + sanPath.length - 2 * i;
    return '' + distance;
}
