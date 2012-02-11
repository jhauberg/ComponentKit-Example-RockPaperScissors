using System;
using ComponentKit.Model;

namespace ComponentKit.Examples.RockPaperScissors.Behaviors {
    /// <summary>
    /// Tends to pick an opponent's previously played hand.
    /// </summary>
    internal class Paranoid : Behavior {
        public override Decision Decide(IEntityRecord opponent) {
            Decision decision = Decision.Undecided;
            History opponentHistory = opponent.GetComponent<History>();

            if (opponentHistory != null) {
                decision = opponentHistory.MostRecentDecision;
            }

            return Decision.Distribute(
                decision.MostInfluencedHand, Influence);
        }

        public override Decision React(IEntityRecord opponent, Decision decision) {
            Decision reaction = Decision.Undecided;
            
            History opponentHistory = opponent.GetComponent<History>();

            if (opponentHistory != null) {
                reaction = opponentHistory.PreviousDecision;
            }

            return Decision.Distribute(
                reaction.MostInfluencedHand, Influence);
        }
    }
}
