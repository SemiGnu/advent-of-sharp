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

func fullOverlap(line string) bool {
	f, s := parseLine(line)
	a, b := lo.Difference(f, s)
	return len(a) == 0 || len(b) == 0
}

func anyOverlap(line string) bool {
	f, s := parseLine(line)
	a, b := lo.Difference(f, s)
	return len(a) < len(f) || len(b) < len(s)
}

func Part1(lines []string) {
	overlaps := lo.CountBy(lines, fullOverlap)
	fmt.Println(overlaps)
}

func Part2(lines []string) {
	overlaps := lo.CountBy(lines, anyOverlap)
	fmt.Println(overlaps)
}
