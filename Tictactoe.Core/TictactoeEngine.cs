using System.Collections.Generic;
using System.Linq;

namespace Tictactoe.Core
{
    public class TictactoeEngine
    {
        private List<List<BoardInsertion>> _board = new List<List<BoardInsertion>>();

        private Dictionary<int, Coordinates> _mapPositionsToCoordinates = new Dictionary<int, Coordinates>() 
        {
            { 
                1, new Coordinates() 
                {
                    Line = 0,
                    Column = 0
                }
            },
            {
                2, new Coordinates()
                {
                    Line = 0,
                    Column = 1
                }
            },
            {
                3, new Coordinates()
                {
                    Line = 0,
                    Column = 2
                }
            },
            {
                4, new Coordinates()
                {
                    Line = 1,
                    Column = 0
                }
            },
            {
                5, new Coordinates()
                {
                    Line = 1,
                    Column = 1
                }
            },
            {
                6, new Coordinates()
                {
                    Line = 1,
                    Column = 2
                }
            },
            {
                7, new Coordinates()
                {
                    Line = 2,
                    Column = 0
                }
            },
            {
                8, new Coordinates()
                {
                    Line = 2,
                    Column = 1
                }
            },
            {
                9, new Coordinates()
                {
                    Line = 2,
                    Column = 2
                }
            }
        };

        private List<List<int>> _combinationOfWins = new List<List<int>>() 
        { 
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 5, 6 },
            new List<int> { 7, 8, 9 },
            new List<int> { 1, 4, 7 },
            new List<int> { 2, 5, 8 },
            new List<int> { 3, 6, 9 },
            new List<int> { 1, 5, 9 },
            new List<int> { 3, 5, 7 },
        };

        private const string X = "X";
        private const string O = "O";

        private ObjectResult _result;

        private const int FirstIndex = 0;
        private const int LastIndex = 2;
        private const int LineColumnSize = 3;

        public TictactoeEngine()
        {
            _board.Add(new List<BoardInsertion>()
            {
                new BoardInsertion()
                {
                    Position = 1,
                    Symbol = string.Empty,
                    coordinates = new Coordinates() 
                    { 
                        Line = 0,
                        Column = 0
                    }
                },
                new BoardInsertion()
                {
                    Position = 2,
                    Symbol = string.Empty,
                    coordinates = new Coordinates()
                    {
                        Line = 0,
                        Column = 1
                    }
                },
                new BoardInsertion()
                {
                    Position = 3,
                    Symbol = string.Empty,
                    coordinates = new Coordinates()
                    {
                        Line = 0,
                        Column = 2
                    }
                }
            });
            _board.Add(new List<BoardInsertion>()
            {
                new BoardInsertion()
                {
                    Position = 4,
                    Symbol = string.Empty,
                    coordinates = new Coordinates()
                    {
                        Line = 1,
                        Column = 0
                    }
                },
                new BoardInsertion()
                {
                    Position = 5,
                    Symbol = string.Empty,
                    coordinates = new Coordinates()
                    {
                        Line = 1,
                        Column = 1
                    }
                },
                new BoardInsertion()
                {
                    Position = 6,
                    Symbol = string.Empty,
                    coordinates = new Coordinates()
                    {
                        Line = 1,
                        Column = 2
                    }
                }
            });
            _board.Add(new List<BoardInsertion>()
            {
                new BoardInsertion()
                {
                    Position = 7,
                    Symbol = string.Empty,
                    coordinates = new Coordinates()
                    {
                        Line = 2,
                        Column = 0
                    }
                },
                new BoardInsertion()
                {
                    Position = 8,
                    Symbol = string.Empty,
                    coordinates = new Coordinates()
                    {
                        Line = 2,
                        Column = 1
                    }
                },
                new BoardInsertion()
                {
                    Position = 9,
                    Symbol = string.Empty,
                    coordinates = new Coordinates()
                    {
                        Line = 2,
                        Column = 2
                    }
                }
            });

            _result = new ObjectResult()
            {
                ValidationMessages = new List<string>()
            };
        }

        private TictactoeEngine IsSymbolValid(string symbol) 
        {
            if (string.IsNullOrWhiteSpace(symbol) || !(symbol == X || symbol == O))
            {
                _result.ValidationMessages.Add("The symbol must be X or O. Empty, white spaces and other symbols are not allowed.");
            }

            return this;
        }

        private TictactoeEngine IsCoordinatesValid(Coordinates coordinates) 
        {
            if (coordinates == null)
            {
                _result.ValidationMessages.Add("Coordinates are null, you must to provide line and colum number to tictactoe board");
                return this;
            }

            if (coordinates.Line < FirstIndex || coordinates.Line > LastIndex
                || coordinates.Column > LastIndex || coordinates.Column < FirstIndex)
            {
                _result.ValidationMessages.Add($"Indexes of {coordinates.Line} and {coordinates.Column} are out of range. You should specify indexes between 0 and 2");
            }

            return this;
        }

        public ObjectResult ExecuteAndVerifyWinner(string symbol, Coordinates coordinates) 
        {
            this.IsSymbolValid(symbol)
                .IsCoordinatesValid(coordinates)
                .ExecuteWithValidation(symbol, coordinates);

            return _result;
        }

        private TictactoeEngine ExecuteWithValidation(string symbol, Coordinates coordinates)
        {
            if (_result.Valid)
            {
                if (string.IsNullOrWhiteSpace(_board[coordinates.Line][coordinates.Column].Symbol))
                {
                    _board[coordinates.Line][coordinates.Column].Symbol = symbol;

                    int currentPosition = _board[coordinates.Line][coordinates.Column].Position;

                    var positionsToCheck = _combinationOfWins.Where(item => item.Contains(currentPosition));

                    int winning = 0;

                    foreach (var sequence in positionsToCheck) 
                    {
                        if (winning == LineColumnSize) 
                        {
                            break;
                        }

                        foreach (var position in sequence) 
                        {
                            _mapPositionsToCoordinates.TryGetValue(position, out var posHelper);

                            if (!string.IsNullOrWhiteSpace(_board[posHelper.Line][posHelper.Column].Symbol)
                                && _board[posHelper.Line][posHelper.Column].Symbol.Equals(symbol))
                            {
                                winning++;
                            }
                            else 
                            {
                                winning = 0;
                                break;
                            }
                        }
                    }

                    _result.GameCompleted = winning == LineColumnSize;
                    if (_result.GameCompleted) 
                    {
                        _result.Winner = $"The {symbol} is the winner!";
                    }
                }
                else
                {
                    _result.ValidationMessages.Add($"The coordinate line = {coordinates.Line} and column = {coordinates.Column} is filled with: {_board[coordinates.Line][coordinates.Column].Symbol}");
                }
            }

            return this;
        }

        public List<List<BoardInsertion>> GetBoard() => _board;
    }
}
