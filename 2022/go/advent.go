package main

import (
	"fmt"
	"os"
	"strings"

	day "github.com/semignu/advent-of-sharp/05"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	d := "05"
	test := 1
	fileName := fmt.Sprintf("%s/input.txt", d)
	if test == 1 {
		fileName = fmt.Sprintf("%s/test.txt", d)
	}
	dat, err := os.ReadFile(fileName)
	check(err)
	file := string(dat)
	lines := strings.Split(file, "\r\n")
	fmt.Println("Day", d, "\r\n------\r\nPart 1:")
	day.Part1(lines)
	fmt.Println("\r\nPart 2:")
	day.Part2(lines)
}
