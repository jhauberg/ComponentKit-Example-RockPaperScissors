using System;
using System.Linq;
using ComponentKit.Model;

namespace ComponentKit.Examples.RockPaperScissors.Behaviors {
    /// <summary>
    /// Rarely picks a losing hand, but does occasionally tie.
    /// </summary>
    /// <remarks>
    /// > Note that influence weighs twice as much on decisions made by this behavior.
    /// </remarks>
    internal class Gifted : Behavior {
        public override Decision Decide(IEntityRecord opponent) {
            Decision trick = Decision.Undecided;
            Outcome outcome = Outcome.Unknown;

            int retries = 0;
            int maxRetries = 5;

            while (outcome == Outcome.Unknown || outcome == Outcome.Loss) {
                // pick any hand and use it to see how opponent would react 
                // > note that it must have some amount of influence to be considered a real decision
                trick = Decision.Next(1);

                Decision reaction = new Decision();

                foreach (Behavior behavior in opponent.GetComponents().OfType<Behavior>()) {
                    reaction += behavior.React(Record, trick);
                }

                outcome = trick.DetermineOutcome(reaction);

                if (retries++ > maxRetries) {
                    break;
                }
            }
            
            // > note that we do not use Decision.Counter on the reaction that resulted in a win, because
            // it was actually the trick hand that caused the win
            return Decision.Distribute(
                trick.MostInfluencedHand, Influence);
        }

        public override Decision React(IEntityRecord opponent, Decision decision) {
            Decision counter = Decision.Win(decision, Influence);

            Hand choices = 
                counter.MostInfluencedHand | 
                decision.MostInfluencedHand;

            return Decision.Next(
                choices, Influence * 2);
        }
    }
}
