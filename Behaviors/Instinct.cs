using System;

namespace ComponentKit.Examples.RockPaperScissors.Behaviors {
    /// <summary>
    /// A gut feeling that may sway a decision one way or another.
    /// </summary>
    /// <remarks>
    /// > This is actually a tiny bugfix. The influence distribution for rock, paper and scissors would often come out with 
    /// two, or even three, being equally influenced. This made the decisions biased, since the properties would be prioritized
    /// depending on the order of comparison. Cue this component - all it does, is add some randomness to every decision. 
    /// 
    /// It makes the described problem a little less frequent, but it definitely doesn't solve the issue entirely.
    /// </remarks>
    internal class Instinct : Unpredictable {
        public Instinct() {
            Influence = 1;
        }
    }
}
