using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComponentKit;
using ComponentKit.Model;

namespace ComponentKit.Examples.RockPaperScissors.Behaviors {
    /// <summary>
    /// Adds influence towards behaviors of a specific type.
    /// </summary>
    internal class Influential<TBehavior> : Behavior where TBehavior : Behavior {
        public override Decision Decide(IEntityRecord opponent) {
            foreach (TBehavior behavior in Record.GetComponents().OfType<TBehavior>()) {
                behavior.Influence += Influence;
            }

            return Decision.Undecided;
        }

        public override Decision React(IEntityRecord opponent, Decision decision) {
            return Decide(opponent);
        }
    }
}
