// Не забудьте перед отправкой изменить в module.exports = function main(game, start) {
// Не деструктурируйте game, ваше решение не будет проходить тесты.

export default function main(game, start) {
    //Direction ways
    //0, right
    //1, down
    //2, left
    //3, up

    let endPoints;
    let countThread = 0; //simple counter
    const state = async (x, y) => {
        return game.state(x, y)
            .then((s) => {
                if (s.finish === true)
                {
                    endPoints = ({ x: x, y: y });
                }
                return s;
            })
    }

    const right = async (x, y) => {
        return await game.right(x, y);
    }

    const left = async (x, y) => {
        return await game.left(x, y);
    }

    const down = async (x, y) => {
        return await game.down(x, y);
    }

    const up = async (x, y) => {
        return await game.up(x, y);
    }

    const GetReverseDirection = (direction) => {
        return (direction + 2) % 4;

    }

    const Step = async (direction, points) => {
        if (direction == 0) {
            await right(points.x++, points.y);
        } else if (direction == 1) {
            await down(points.x, points.y++);
        } else if (direction == 2) {
            await left(points.x--, points.y);
        } else if (direction == 3) {
            await up(points.x, points.y--);
        }
    }


    const NewWay = async (game, direction, points, first = false) => {
        countThread++;
        let newPoints = {x: points.x, y: points.y};
        let newDirection = direction;
        let reverseDirection = GetReverseDirection(newDirection);

        let currentState = await state(newPoints.x, newPoints.y);
        let countWay = currentState.bottom + currentState.top + currentState.right + currentState.left;

        if (!currentState.start || first == false) {
            await Step(newDirection, newPoints);
            currentState = await state(newPoints.x, newPoints.y);
            countWay = currentState.bottom + currentState.top + currentState.right + currentState.left - 1;
        }

        while (countWay >= 1 && endPoints === undefined && newPoints.y < 30) {

            if (countWay != 0) {

                if (currentState.right && reverseDirection != 0) {
                    if (countWay > 1) {
                        NewWay(game, 0, newPoints);
                    } else {
                        newDirection = 0;
                    }
                    countWay--;
                }
                if (currentState.bottom && reverseDirection != 1) {
                    if (countWay > 1) {
                        NewWay(game, 1, newPoints);
                    } else {
                        newDirection = 1;
                    }
                    countWay--;
                }
                if (currentState.left && reverseDirection != 2) {
                    if (countWay > 1) {
                        NewWay(game, 2, newPoints);
                    } else {
                        newDirection = 2;
                    }
                    countWay--;
                }
                if (currentState.top && reverseDirection != 3) {
                    if (countWay > 1) {
                        NewWay(game, 3, newPoints);
                    } else {
                        newDirection = 3;
                    }
                    countWay--;
                }
                await Step(newDirection, newPoints);
                currentState = await state(newPoints.x, newPoints.y);

            }

            reverseDirection = GetReverseDirection(newDirection);
            countWay = currentState.bottom + currentState.top + currentState.right + currentState.left - 1;
        }

        if (first)
        {
            while (endPoints === undefined)
            {
                await new Promise(resolve => setTimeout(resolve, 100));
            }
            return endPoints;
        }
        countThread--;

    }


    const StartGame = async () => {
        return NewWay(game, 0, start, true);
    }

    return StartGame();
}