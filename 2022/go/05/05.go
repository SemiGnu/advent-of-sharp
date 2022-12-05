package day05

import (
	"fmt"
	"regexp"
	"strconv"

	"github.com/samber/lo"
)

func initStack(line string) map[int]*stack[rune] {
	s := make(map[int]*stack[rune])
	for i := 1; i <= 9; i++ {
		s[i] = &stack[rune]{_stack: make([]rune, 300), position: -1}
	}
	return s
}

func parseStackLine(line string, stack map[int]*stack[rune]) {
	chunks := lo.Chunk([]rune(line), 4)
	for i, chunk := range chunks {
		if chunk[0] != ' ' {
			stack[i+1].push(chunk[1])
		}
	}
}

type instruction struct {
	amount int
	from   int
	to     int
}

type stack[T any] struct {
	_stack   []T
	position int
}

func (s *stack[T]) pop() T {
	if s.position < 0 {
		panic("stack is empty")
	}
	s.position = s.position - 1
	return s._stack[(s.position + 1)]
}

func (s *stack[T]) push(value T) {
	if s.position == len(s._stack)-1 {
		panic("stack is full")
	}
	s.position = s.position + 1
	s._stack[s.position] = value
}

func parse(lines []string) (map[int]*stack[rune], []instruction) {
	instructionsLines := lo.DropWhile(lines, func(line string) bool { return len(line) == 0 || line[0] != 'm' })

	regex, _ := regexp.Compile("move (\\d*) from (\\d*) to (\\d*)")

	instructions := lo.Map(instructionsLines, func(line string, _ int) instruction {
		r := lo.Map(regex.FindStringSubmatch(line), func(s string, _ int) int {
			n, _ := strconv.Atoi(s)
			return int(n)
		})
		return instruction{amount: r[1], from: r[2], to: r[3]}
	})

	stackLines := lo.DropRightWhile(lines, func(line string) bool { return len(line) == 0 || line[0] != '[' })
	stackLines = lo.Reverse(stackLines)
	stack := initStack(lines[0])
	lo.ForEach(stackLines, func(s string, _ int) {
		parseStackLine(s, stack)
	})

	return stack, instructions
}

func execute(stack map[int]*stack[rune], instructions []instruction) {
	for _, instruction := range instructions {
		for i := 0; i < instruction.amount; i++ {
			stack[instruction.to].push(stack[instruction.from].pop())
		}
	}
}

func execute2(s map[int]*stack[rune], instructions []instruction) {
	for _, instruction := range instructions {
		temp := stack[rune]{_stack: make([]rune, 300), position: -1}
		for i := 0; i < instruction.amount; i++ {
			temp.push(s[instruction.from].pop())
		}
		for i := 0; i < instruction.amount; i++ {
			s[instruction.to].push(temp.pop())
		}
	}
}

func getMessage(stack map[int]*stack[rune]) string {
	message := ""
	for i := 1; i <= 9; i++ {
		if stack[i].position >= 0 {
			r := stack[i].pop()
			message += string(r)
		}
	}
	return message
}

func Part1(lines []string) {
	stack, instructions := parse(lines)
	execute(stack, instructions)
	message := getMessage(stack)
	fmt.Println(message)
}

func Part2(lines []string) {
	stack, instructions := parse(lines)
	execute2(stack, instructions)
	message := getMessage(stack)
	fmt.Println(message)
}
