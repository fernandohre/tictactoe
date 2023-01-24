namespace Tictactoe.Core.Tests
{
    public class TictactoeEngineTests
    {
        private TictactoeEngine _tictactoe;

        [SetUp]
        public void Setup()
        {
            _tictactoe = new TictactoeEngine();
        }

        [Test]
        public void ExecuteWithValidation_HappyPath_TheWinnerIsO()
        {
            _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 0, Column = 0 });
            _tictactoe.ExecuteAndVerifyWinner("O", new Coordinates() { Line = 1, Column = 1 });
            _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 0, Column = 2 });
            _tictactoe.ExecuteAndVerifyWinner("O", new Coordinates() { Line = 0, Column = 1 });
            _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 2, Column = 1 });
            _tictactoe.ExecuteAndVerifyWinner("O", new Coordinates() { Line = 1, Column = 0 });
            _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 2, Column = 0 });
            var result = _tictactoe.ExecuteAndVerifyWinner("O", new Coordinates() { Line = 1, Column = 2 });

            Assert.IsTrue(result.GameCompleted && result.Winner.Equals("The O is the winner!"));
        }

        [Test]
        public void ExecuteWithValidation_HappyPath_TheWinnerIsX()
        {
            _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 1, Column = 1 });
            _tictactoe.ExecuteAndVerifyWinner("O", new Coordinates() { Line = 0, Column = 2 });
            _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 2, Column = 0 });
            _tictactoe.ExecuteAndVerifyWinner("O", new Coordinates() { Line = 0, Column = 1 });
            _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 0, Column = 0 });
            _tictactoe.ExecuteAndVerifyWinner("O", new Coordinates() { Line = 1, Column = 0 });
            var result = _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 2, Column = 2 });

            Assert.IsTrue(result.GameCompleted && result.Winner.Equals("The X is the winner!"));
        }

        [Test]
        public void ExecuteWithValidation_HappyPath_TheWinnerIsXVertically()
        {
            _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 0, Column = 2 });
            _tictactoe.ExecuteAndVerifyWinner("O", new Coordinates() { Line = 1, Column = 1 });
            _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 2, Column = 0 });
            _tictactoe.ExecuteAndVerifyWinner("O", new Coordinates() { Line = 2, Column = 2 });
            _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 0, Column = 0 });
            _tictactoe.ExecuteAndVerifyWinner("O", new Coordinates() { Line = 0, Column = 1 });
            _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 1, Column = 0 });
            var result = _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 2, Column = 2 });

            Assert.IsTrue(result.GameCompleted && result.Winner.Equals("The X is the winner!"));
        }

        [Test]
        public void ExecuteWithValidation_WrongPath_TryingToFillUsedSlot() 
        {
            _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = 1, Column = 1 });
            var result = _tictactoe.ExecuteAndVerifyWinner("O", new Coordinates() { Line = 1, Column = 1 });

            result.ValidationMessages.ForEach(msg => Console.WriteLine(msg));

            Assert.IsFalse(result.Valid);
        }

        [Test]
        public void ExecuteWithValidation_WrongPath_FillingSymbolDifferentThanX_Or_O()
        {
            var result = _tictactoe.ExecuteAndVerifyWinner("A", new Coordinates() { Line = 1, Column = 1 });

            result.ValidationMessages.ForEach(msg => Console.WriteLine(msg));

            Assert.IsFalse(result.Valid);
        }

        [Test]
        public void ExecuteWithValidation_WrongPath_CoordinatesOutOfRange()
        {
            var result = _tictactoe.ExecuteAndVerifyWinner("X", new Coordinates() { Line = -1, Column = -1 });

            result.ValidationMessages.ForEach(msg => Console.WriteLine(msg));

            Assert.IsFalse(result.Valid);
        }


        [Test]
        public void ExecuteWithValidation_WrongPath_SymbolAndCoordinatesValidation()
        {
            var result = _tictactoe.ExecuteAndVerifyWinner("A", new Coordinates() { Line = -1, Column = -1 });

            result.ValidationMessages.ForEach(msg => Console.WriteLine(msg));

            int quantityOfMsgValidation = 2;

            Assert.IsTrue(result.ValidationMessages.Count == quantityOfMsgValidation);
        }

        [Test]
        public void ExecuteWithValidation_WrongPath_CoordinatesAreNullAndSymbolInvalid()
        {
            var result = _tictactoe.ExecuteAndVerifyWinner("A", null);

            result.ValidationMessages.ForEach(msg => Console.WriteLine(msg));

            Assert.False(result.Valid, result.ValidationMessages.ToString());
        }
    }
}