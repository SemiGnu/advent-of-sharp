package day09

import (
	"fmt"
	"image/color"
	"math"
	"strconv"

	"github.com/fzipp/canvas"
	"github.com/samber/lo"
)

type vector struct {
	x int
	y int
}

func parseLine(line string, _ int) []vector {
	n, _ := strconv.Atoi(line[2 : len(line)-1])
	moves := make([]vector, n)
	dir := vector{1, 0}
	if line[0] == 'L' {
		dir = vector{-1, 0}
	}
	if line[0] == 'U' {
		dir = vector{0, 1}
	}
	if line[0] == 'D' {
		dir = vector{0, -1}
	}
	for i := 0; i < n; i++ {
		moves[i] = dir
	}
	return moves
}

func executeMove(hPos vector, tPos vector, move vector) (vector, vector) {
	hPos.x += move.x
	hPos.y += move.y
	if math.Abs(float64(hPos.x-tPos.x))+math.Abs(float64(hPos.y-tPos.y)) > 3 {
		tPos.x += move.x
		tPos.y += move.y
	}
	if hPos.x-tPos.x < -1 {
		tPos.x -= 1
		tPos.y = hPos.y
	}
	if hPos.x-tPos.x > 1 {
		tPos.x += 1
		tPos.y = hPos.y
	}
	if hPos.y-tPos.y < -1 {
		tPos.y -= 1
		tPos.x = hPos.x
	}
	if hPos.y-tPos.y > 1 {
		tPos.y += 1
		tPos.x = hPos.x
	}
	return hPos, tPos
}

func Part1(lines []string) {
	moves := lo.FlatMap(lines, parseLine)
	hPos, tPos := vector{0, 0}, vector{0, 0}
	visited := make(map[vector]int)
	visited[tPos] = 0
	for _, m := range moves {
		hPos, tPos = executeMove(hPos, tPos, m)
		visited[tPos] = 0
	}

	fmt.Println(lo.MaxBy(lo.Keys(visited), func(a vector, b vector) bool { return a.y > b.y }))
	fmt.Println(len(visited))

	canvas.ListenAndServe(":80",
		func(ctx *canvas.Context) { run(ctx, moves) },
		canvas.Size(1000, 1000),
		canvas.Title("Example 1: Drawing"),
		canvas.EnableEvents(canvas.KeyDownEvent{}),
	)

}

func Part2(lines []string) {
	fmt.Println("tbd")
}

func run(ctx *canvas.Context, moves []vector) {
	d := &demo{}
	i := 0
	for !d.quit {
		select {
		case event := <-ctx.Events():
			d.handle(event)
		default:
			if d.next {
				d.update()
				d.draw(ctx)
				d.next = false
			}
		}
	}
}

type demo struct {
	quit  bool
	next  bool
	hPos  vector
	tPos  vector
	drawn map[vector]int
}

func (d *demo) handle(event canvas.Event) {
	switch e := event.(type) {
	case canvas.CloseEvent:
		d.quit = true
	case canvas.KeyDownEvent:
		d.next = true
		test := e.KeyboardEvent.Key
		fmt.Println(test)
	}
}

func (d *demo) update(move vector) {
	d.hPos, d.tPos = executeMove(d.hPos, d.tPos, move)
	d.drawn[d.tPos] = 0
}

func (d *demo) draw(ctx *canvas.Context) {
	ctx.SetFillStyle(color.RGBA{R: 200, A: 16})
	ctx.FillRect(10, 10, 50, 50)
	// ...
	ctx.Flush()
}
