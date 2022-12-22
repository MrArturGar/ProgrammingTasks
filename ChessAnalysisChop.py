class Checker:
	x = 0
	y = 0

boardX = None
boardY = None
WhiteCount = None
BlackCount = None
Whites = []
Blacks = []
WhiteMoves = False

def GetInput():
	global boardX, boardY, WhiteCount, BlackCount, Whites, Blacks, WhiteMoves
	step = 0
	Lines = open('input.txt', 'r')

	for line in Lines:
		line = line.replace('\n', '')
		if step == 0:
			boardX = int(line.split()[0])
			boardY = int(line.split()[1])

		if step == 1:
			WhiteCount = int(line)

		if step == 2:
			pos = line.split()
			if len(pos) == 2:
				check = Checker()
				check.x = int(pos[0])
				check.y = int(pos[1])
				Whites.append(check)
				continue
			step += 1

		if step == 3:
			BlackCount = int(line)

		if step == 4:
			pos = line.split()
			if len(pos) == 2:
				check = Checker()
				check.x = int(pos[0])
				check.y = int(pos[1])
				Blacks.append(check)
				continue
			step += 1

		if step == 5:
			if line == 'white':
				WhiteMoves = True
			else:
				WhiteMoves = False

		step += 1

def CheckPos(x, y, checkers):
	if x > boardX or y > boardY or x < 1 or y < 1:
		return True

	for checker in checkers:
		if checker.x == x and checker.y == y:
			return True
	return False


def CheckNeighbor(master, slave):
	for checker in master:
		shiftX = -1
		while shiftX <= 1:
			shiftY = -1
			while shiftY <= 1:
				if shiftX != 0 and shiftY != 0:
					result = CheckPos(checker.x + shiftX, checker.y + shiftY, slave)
					
					if result == True:
						result1 = CheckPos(checker.x + shiftX*2, checker.y + shiftY*2, slave + master)
						if result1 == False:
							return True
				shiftY +=1;
			shiftX +=1;
	return False


GetInput()

result = False
if WhiteMoves == True:
	result = CheckNeighbor(Whites, Blacks)
else:
	result = CheckNeighbor(Blacks, Whites)
	
file = open('output.txt', 'w')
if result == True:
	file.write("Yes")
else:
	file.write("No")
file.close()