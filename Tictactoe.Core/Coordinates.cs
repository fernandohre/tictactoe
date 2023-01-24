namespace Tictactoe.Core
{
    public class Coordinates
    {
        public const int FIRSTLINE = 0;
        public const int SECONDLINE = 1;
        public const int THIRDLINE = 2;

        public const int FIRSTCOLUMN = 0;
        public const int SECONDCOLUMN = 1;
        public const int THIRDCOLUMN = 2;

        public int Line { get; set; }
        public int Column { get; set; }
    }
}
