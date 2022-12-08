package day06

import (
	"fmt"

	"github.com/samber/lo"
)

func findMarker(line string, markerSize int) int {
	message := []rune(line)
	buffer := message[:markerSize]
	for idx, r := range message[markerSize:] {
		buffer[idx%markerSize] = r
		if len(lo.Uniq(buffer)) == markerSize {
			return idx + markerSize + 1
		}
	}
	panic("!")
}

func Part1(lines []string) {
	lo.ForEach(lines, func(line string, _ int) { fmt.Println(findMarker(line, 4)) })
}

func Part2(lines []string) {
	lo.ForEach(lines, func(line string, _ int) { fmt.Println(findMarker(line, 14)) })
}
