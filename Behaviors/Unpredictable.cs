using System;

namespace ComponentKit.Examples.RockPaperScissors.Behaviors {
    /// <summary>
    /// Always picks a random hand.
    /// </summary>
    internal class Unpredictable : Behavior {
        public override Decision Decide(IEntityRecord opponent) {
            return Decision.Next(
                Influence);
        }

        public override Decision React(IEntityRecord opponent, Decision decision) {
            return Decide(opponent);
        }
    }
}
