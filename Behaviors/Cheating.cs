using System;
using System.Linq;
using ComponentKit.Model;

namespace ComponentKit.Examples.RockPaperScissors.Behaviors {
    /// <summary>
    /// Always picks a winning hand, and adds a large amount of influence towards it.
    /// </summary>
    internal class Cheating : Behavior {
        public int AdditionalInfluence {
            get;
            set;
        }

        public Cheating() {
            AdditionalInfluence = 100;
        }

        public override Decision Decide(IEntityRecord opponent) {
            Decision trick = Decision.Undecided;
            Outcome outcome = Outcome.Unknown;

            int retries = 0;
            int maxRetries = 5;

            while (outcome == Outcome.Unknown || outcome == Outcome.Loss || outcome == Outcome.Tie) {
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
                trick.MostInfluencedHand, Influence + AdditionalInfluence);
        }

        public override Decision React(IEntityRecord opponent, Decision decision) {
            return Decision.Win(
                decision, Influence + AdditionalInfluence);
        }
    }
}
