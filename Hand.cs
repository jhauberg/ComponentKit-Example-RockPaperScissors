using System;

namespace ComponentKit.Examples.RockPaperScissors {
    [Flags]
    internal enum Hand : int {
        NotSpecified = 0,
        Rock = 1,
        Paper = 2,
        Scissors = 4
    }
}
