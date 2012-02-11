using System;

namespace ComponentKit.Examples.RockPaperScissors.Behaviors {
    /// <summary>
    /// Stubborn and likely to pick a losing hand.
    /// </summary>
    internal class Predictable : Stubborn {
        /// <summary>
        /// Reacts on an opponent's decision by always picking a losing hand.
        /// </summary>
        public override Decision React(IEntityRecord opponent, Decision decision) {
            return Decision.Loss(
                decision, Influence);
        }
    }
}
