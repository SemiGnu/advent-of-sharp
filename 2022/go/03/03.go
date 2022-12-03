package day03

import (
	"fmt"

	"github.com/samber/lo"
)

func findItem(line string, _ int) rune {
	f, s := line[:len(line)/2], line[len(line)/2:]
	isct := lo.Intersect([]rune(f), []rune(s))
	return isct[0]
}

func getPriority(r rune) int {
	if r >= 'a' {
		return int(r - 'a' + 1)
	}
	return int(r - 'A' + 27)
}

func findBadge(lines []string, _ int) rune {
	isct := lo.Intersect([]rune(lines[0]), []rune(lines[1]))
	isct = lo.Intersect(isct, []rune(lines[2]))
	return isct[0]
}

func Part1(lines []string) {
	items := lo.Map(lines, findItem)
	prioritySum := lo.SumBy(items, getPriority)
	fmt.Println(prioritySum)
}

func Part2(lines []string) {
	groups := lo.Chunk(lines, 3)
	badges := lo.Map(groups, findBadge)
	prioritySum := lo.SumBy(badges, getPriority)
	fmt.Println(prioritySum)
}
