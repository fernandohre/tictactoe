using System.Collections.Generic;
using System.Linq;

namespace Tictactoe.Core
{
    public class ObjectResult
    {
        public bool Valid { get => !ValidationMessages.Any(); }

        public string Winner { get; set; }

        public List<string> ValidationMessages { get; set; }

        public bool GameCompleted { get; set; }
    }
}
