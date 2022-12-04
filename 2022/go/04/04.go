package day03

import (
	"fmt"
	"regexp"
	"strconv"

	"github.com/samber/lo"
)

func makeRange(start int, end int) []int {
	length := end - start + 1
	slice := make([]int, length)
	for i := 0; i < length; i++ {
		slice[i] = i + start
	}
	return slice
}

func parseLine(line string) ([]int, []int) {
	regex, _ := regexp.Compile("(\\d*)-(\\d*),(\\d*)-(\\d*)")
	r := lo.Map(regex.FindStringSubmatch(line), func(s string, _ int) int {
		n, _ := strconv.Atoi(s)
		return int(n)
	})
	return makeRange(r[1], r[2]), makeRange(r[3], r[4])
}

func fullOverlap(f []int, s []int) bool {
	a, b := lo.Difference(f, s)
	return len(a) == 0 || len(b) == 0
}

func anyOverlap(f []int, s []int) bool {
	a, b := lo.Difference(f, s)
	return len(a) < len(f) || len(b) < len(s)
}

func Part1(lines []string) {
	contains := lo.Filter(lines, func(line string, _ int) bool {
		return fullOverlap(parseLine(line))
	})
	fmt.Println(len(contains))
}

func Part2(lines []string) {
	contains := lo.Filter(lines, func(line string, _ int) bool {
		return anyOverlap(parseLine(line))
	})
	fmt.Println(len(contains))
}
