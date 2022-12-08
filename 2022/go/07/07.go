package day07

import (
	"fmt"

	"github.com/samber/lo"
)

type item interface {
	getSize() int
}

type file struct {
	size int
	name string
}
type directory struct {
	contents []item
	name     string
}

func (r file) getSize() int {
	return r.size
}
func (r directory) getSize() int {
	return lo.SumBy(r.contents, func(i item) int { return i.getSize() })
}

func Part1(lines []string) {
	fmt.Println("tbd")
}

func Part2(lines []string) {
	fmt.Println("tbd")
}
