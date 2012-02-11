using System;
using System.Linq;
using ComponentKit.Model;
using ComponentKit.Examples.RockPaperScissors.Behaviors;
using System.Collections.Generic;

namespace ComponentKit.Examples.RockPaperScissors {
    internal class Simulation : IDisposable {
        public void Run(IEntityRecord a, IEntityRecord b) {
            Console.WriteLine("{0}\n", a);
            Console.WriteLine("  against\n");
            Console.WriteLine("{0}\n", b);

            IDictionary<IEntityRecord, int> wins = 
                new Dictionary<IEntityRecord, int>();

            int turns = 0;
            int ties = 0;

            while (turns < 3) {
                // determine who gets to go first
                IEntityRecord decider = 
                    turns % 2 != 0 ?
                        a : b;

                // the other player automatically becomes the target  (i.e., he gets to know which 
                // hand his opponent chose and can react accordingly depending on behavior)
                IEntityRecord target = 
                    decider.Equals(a) ?
                        b : a;

                History history = decider.GetComponent<History>();

                // determine the hand decision by the influence of each behavior
                Decision decision = new Decision();

                foreach (Behavior behavior in decider.GetComponents().OfType<Behavior>()) {
                    decision += behavior.Decide(target);
                }

                // store the decision in the player's history stack - this is useful because some behaviors
                // use previous decisions to determine what to play next
                history.Push(decision);

                history = target.GetComponent<History>();

                // determine which hand to play in reaction to the opponent's decision
                Decision reaction = new Decision();

                foreach (Behavior behavior in target.GetComponents().OfType<Behavior>()) {
                    reaction += behavior.React(decider, decision);
                }

                history.Push(reaction);
   
                Outcome outcome = decision.DetermineOutcome(reaction);

                if (outcome != Outcome.Unknown) {
                    if (outcome != Outcome.Tie) {
                        IEntityRecord winner =
                            outcome == Outcome.Win ?
                                decider :
                                target;

                        if (wins.ContainsKey(winner)) {
                            wins[winner] += 1;
                        } else {
                            wins[winner] = 1;
                        }
                    } else {
                        ties++;
                    }
                }

                turns++;
            }
            
            Console.WriteLine("Results over {0} turns {1}\n", turns, ties > 0 ?
                String.Format("(with {0} {1}):", ties, ties == 1 ? "tie" : "ties") : string.Empty);

            foreach (IEntityRecord entity in wins.Keys.OrderByDescending(e => wins[e])) {
                Console.WriteLine(" '{0}' won {1} {2}", entity.Name, wins[entity], wins[entity] == 1 ? "time" : "times");
            }
        }

        public void Dispose() {
            
        }
    }
}
