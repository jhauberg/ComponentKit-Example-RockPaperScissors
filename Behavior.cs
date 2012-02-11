using System;
using ComponentKit.Model;

namespace ComponentKit.Examples.RockPaperScissors {
    internal abstract class Behavior : DependencyComponent {
        const int DefaultMinInfluence = 1;
        const int DefaultMaxInfluence = 5;

        public int Influence {
            get;
            set;
        }

        public Behavior() {
            /// > Think of influence as a rating for how much effect a behavior has on a players' final decision.
            Influence = Roll.Next(
                DefaultMinInfluence,
                DefaultMaxInfluence + 1);
        }

        public abstract Decision Decide(IEntityRecord opponent);
        public abstract Decision React(IEntityRecord opponent, Decision decision);
    }
}
