package day02

import (
	"fmt"

	"github.com/samber/lo"
)

func scoreLine(line string) int {
	f, s := line[0]-'A', line[2]-'X'
	return int(((s-f+4)%3)*3 + 1 + s)
}

func scoreLine2(line string) int {
	f, s := line[0]-'A', line[2]-'X'
	return int(((f + s + 2) % 3) + 1 + s*3)
}

func Part1(lines []string) {
	score := lo.SumBy(lines, scoreLine)
	fmt.Println(score)
}

func Part2(lines []string) {
	score := lo.SumBy(lines, scoreLine2)
	fmt.Println(score)
}
