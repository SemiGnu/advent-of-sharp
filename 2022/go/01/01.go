package day01

import (
	"fmt"
	"sort"
	"strconv"

	"github.com/samber/lo"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func getElves(lines []string) []int {
	input := lo.Map(lines, func(l string, index int) int {
		if l == "" {
			return -1
		}
		_int, err := strconv.Atoi(l)
		check(err)
		return _int
	})

	elves := lo.Reduce(input, func(agg []int, item, index int) []int {
		if item == -1 {
			agg = append(agg, 0)
			return agg
		}
		agg[len(agg)-1] += item
		return agg
	}, []int{0})

	return elves
}

func Part1(lines []string) {
	elves := getElves(lines)
	max := lo.Max(elves)
	fmt.Println(max)
}

func Part2(lines []string) {
	elves := getElves(lines)
	sort.Ints(elves)
	sum := lo.Sum(elves[len(elves)-3:])
	fmt.Println(sum)
}
