using System;

namespace ComponentKit.Examples.RockPaperScissors.Behaviors {
    /// <summary>
    /// Prefers the heavy-hitters.
    /// </summary>
    internal class Brute : Behavior {
        /// <summary>
        /// Picks either rock or scissors.
        /// </summary>
        public override Decision Decide(IEntityRecord opponent) {
            return Decision.Next(
                Hand.Rock | Hand.Scissors, Influence);
        }

        public override Decision React(IEntityRecord opponent, Decision decision) {
            return Decide(opponent);
        }
    }
}
