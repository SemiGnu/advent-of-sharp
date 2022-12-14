package day07

import (
	"fmt"
	"regexp"
	"strconv"

	"github.com/samber/lo"
)

type item interface {
	getName() string
	getSizeMax100k(count *int) int
	getSizeMin(min *int, target int) int
}

type file struct {
	name string
	size int
}
type directory struct {
	name     string
	contents map[string]item
	parent   *directory
}

func (r file) getSizeMax100k(count *int) int {
	return r.size
}
func (r file) getSizeMin(min *int, target int) int {
	return r.size
}
func (r file) getName() string {
	return r.name
}
func (r directory) getSizeMax100k(count *int) int {
	size := lo.SumBy(lo.Values(r.contents), func(i item) int { return i.getSizeMax100k(count) })
	if size <= 100000 {
		*count += size
	}
	return size
}
func (r directory) getSizeMin(min *int, target int) int {
	size := lo.SumBy(lo.Values(r.contents), func(i item) int { return i.getSizeMin(min, target) })
	if target <= size && size <= *min {
		*min = size
	}
	return size
}
func (r directory) getName() string {
	return r.name
}

func parseLine(line string, parent *directory) item {
	if line[0:3] == "dir" {
		d := new(directory)
		d.name = line[4:]
		d.contents = make(map[string]item)
		d.parent = parent
		return d
	}
	regex, _ := regexp.Compile("(\\d+) (.*)")
	matches := regex.FindStringSubmatch(line)
	n, _ := strconv.Atoi(matches[1])
	return file{matches[2], n}
}

func buildTree(lines []string) directory {
	initialDirectory := directory{"/", make(map[string]item), nil}
	currentDirectory := &initialDirectory
	for i := 1; i < len(lines); i++ {
		if lines[i] == "$ ls" {
			i++
			j := i
			for j < len(lines) && lines[j][0] != '$' {
				j++
			}
			items := lo.Map(lines[i:j], func(line string, _ int) item { return parseLine(line, currentDirectory) })
			currentDirectory.contents = lo.Associate(items, func(i item) (string, item) { return i.getName(), i })
			i = j - 1
		} else if lines[i] == "$ cd .." {
			currentDirectory = currentDirectory.parent
		} else {
			newDirectory := lines[i][5:]
			currentDirectory, _ = currentDirectory.contents[newDirectory].(*directory)
		}
	}
	return initialDirectory
}

func Part1(lines []string) {
	root := buildTree(lines)
	count := new(int)
	root.getSizeMax100k(count)
	fmt.Println(*count)
}

func Part2(lines []string) {
	root := buildTree(lines)
	count := new(int)
	rootSize := root.getSizeMax100k(count)
	deleteSize := rootSize - 40_000_000
	min := new(int)
	*min = rootSize
	root.getSizeMin(min, deleteSize)
	fmt.Println(*min)
}
