package main

import (
	"os"
	"strings"

	day "github.com/semignu/advent-of-sharp/01"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	dat, err := os.ReadFile("01/test.txt")
	check(err)
	file := string(dat)
	lines := strings.Split(file, "\r\n")
	day.Part1(lines)
	day.Part2(lines)
}
