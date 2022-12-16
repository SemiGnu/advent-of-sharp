package day08

import (
	"fmt"

	"github.com/samber/lo"
)

type pos struct {
	h int
	v int
}

func parseLine(line string, _ int) []int {
	return lo.Map([]rune(line), func(r rune, _ int) int { return int(r - '0') })
}

func countVisibleInRow(seenTrees map[pos]int, grid [][]int, rowIdx int) {
	l := len(grid) - 1
	seenTrees[pos{rowIdx, 0}] = grid[rowIdx][0]
	seenTrees[pos{rowIdx, l}] = grid[rowIdx][l]
	leftMax, rightMax := grid[rowIdx][0], grid[rowIdx][l]
	for left := 1; left <= l; left++ {
		if grid[rowIdx][left] > leftMax {
			leftMax = grid[rowIdx][left]
			seenTrees[pos{rowIdx, left}] = grid[rowIdx][left]
		}
	}
	for right := l - 1; right >= 0; right-- {
		if grid[rowIdx][right] > rightMax {
			rightMax = grid[rowIdx][right]
			seenTrees[pos{rowIdx, right}] = grid[rowIdx][right]
		}
	}
}

func countVisibleInColumn(seenTrees map[pos]int, grid [][]int, columnIdx int) {
	l := len(grid) - 1
	seenTrees[pos{0, columnIdx}] = grid[0][columnIdx]
	seenTrees[pos{l, columnIdx}] = grid[l][columnIdx]
	leftMax, rightMax := grid[0][columnIdx], grid[l][columnIdx]
	for left := 1; left <= l; left++ {
		if grid[left][columnIdx] > leftMax {
			leftMax = grid[left][columnIdx]
			seenTrees[pos{left, columnIdx}] = grid[left][columnIdx]
		}
	}
	for right := l - 1; right >= 0; right-- {
		if grid[right][columnIdx] > rightMax {
			rightMax = grid[right][columnIdx]
			seenTrees[pos{right, columnIdx}] = grid[right][columnIdx]
		}
	}
}

func countVisible(grid [][]int) int {
	seenTrees := make(map[pos]int)
	for i, _ := range grid {
		countVisibleInRow(seenTrees, grid, i)
		countVisibleInColumn(seenTrees, grid, i)
	}
	return len(seenTrees)
}

func getScenicScore(grid [][]int, v int, h int) int {
	l, r, u, d := h-1, h+1, v-1, v+1
	val := grid[v][h]
	for l > 0 && grid[v][l] < val {
		l--
	}
	for r < len(grid)-1 && grid[v][r] < val {
		r++
	}
	for u > 0 && grid[u][h] < val {
		u--
	}
	for d < len(grid)-1 && grid[d][h] < val {
		d++
	}
	score := (h - l) * (r - h) * (v - u) * (d - v)
	return score

}

func Part1(lines []string) {
	grid := lo.Map(lines, parseLine)
	vis := countVisible(grid)
	fmt.Println(vis)
}

func Part2(lines []string) {
	grid := lo.Map(lines, parseLine)
	max := 0
	for i := 0; i < len(grid); i++ {
		for j := 0; j < len(grid); j++ {
			score := getScenicScore(grid, j, i)
			if score > max {
				max = score
			}
		}
	}
	fmt.Println(max)
}
