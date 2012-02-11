using System;

namespace ComponentKit.Examples.RockPaperScissors.Behaviors {
    /// <summary>
    /// Consistently picks the same hand.
    /// </summary>
    internal class Stubborn : Behavior {
        protected Hand Hand {
            get;
            private set;
        }

        public Stubborn() {
            int n = Roll.Next(0, 3);

            Hand = n == 0 ?
                Hand.Rock : n == 1 ?
                    Hand.Paper : Hand.Scissors;
        }

        public override Decision Decide(IEntityRecord opponent) {
            return Decision.Distribute(
                Hand, Influence);
        }

        public override Decision React(IEntityRecord opponent, Decision decision) {
            return Decide(opponent);
        }
    }
}
