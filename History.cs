using System;
using System.Collections.Generic;
using ComponentKit.Model;

namespace ComponentKit.Examples.RockPaperScissors {
    /// <summary>
    /// Keeps track of previous decisions.
    /// </summary>
    internal class History : Component {
        IList<Decision> _decisions =
            new List<Decision>();

        public void Push(Decision decision) {
            _decisions.Add(decision);
        }

        public int Count {
            get {
                return _decisions.Count;
            }
        }

        public Decision MostRecentDecision {
            get {
                return this[Count - 1];
            }
        }

        public Decision PreviousDecision {
            get {
                return this[Count - 2];
            }
        }

        public Decision this[int index] {
            get {
                if (index >= 0 && _decisions.Count > index) {
                    return _decisions[index];
                }

                return Decision.Undecided;
            }
        }
    }
}
