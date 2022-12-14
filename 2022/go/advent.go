package main

import (
	"fmt"
	"os"
	"strings"

	day "github.com/semignu/advent-of-sharp/07"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	d := "07"
	test := 0
	fileName := fmt.Sprintf("%s/input.txt", d)
	if test == 1 {
		fileName = fmt.Sprintf("%s/test.txt", d)
	}
	dat, err := os.ReadFile(fileName)
	check(err)
	file := string(dat)
	lines := strings.Split(file, "\n")
	fmt.Println("Day", d, "\r\n------\r\nPart 1:")
	day.Part1(lines)
	fmt.Println("\r\nPart 2:")
	day.Part2(lines)
}
