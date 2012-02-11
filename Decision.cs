using System;
using System.Collections.Generic;

namespace ComponentKit.Examples.RockPaperScissors {
    internal struct Decision {
        public static readonly Decision Undecided =
            new Decision();

        public int InfluenceTowardsRock {
            get;
            private set;
        }

        public int InfluenceTowardsPaper {
            get;
            private set;
        }

        public int InfluenceTowardsScissors {
            get;
            private set;
        }

        public Hand MostInfluencedHand {
            get {
                if (InfluenceTowardsRock == 0 && 
                    InfluenceTowardsPaper == 0 && 
                    InfluenceTowardsScissors == 0) {
                    return Hand.NotSpecified;
                }

                Tuple<Hand, int> hand =
                    InfluenceTowardsRock > InfluenceTowardsPaper ?
                        Tuple.Create(Hand.Rock, InfluenceTowardsRock) :
                        Tuple.Create(Hand.Paper, InfluenceTowardsPaper);

                Hand mostInfluencedHand = hand.Item1;

                if (InfluenceTowardsScissors > hand.Item2) {
                    mostInfluencedHand = Hand.Scissors;
                }

                return mostInfluencedHand;
            }
        }

        public Outcome DetermineOutcome(Decision otherDecision) {
            Outcome outcome = Outcome.Unknown;

            Hand ha = MostInfluencedHand;
            Hand hb = otherDecision.MostInfluencedHand;

            if (ha == Hand.NotSpecified || hb == Hand.NotSpecified) {
                return outcome;
            }

            if (ha != hb) {
                switch (ha) {
                    default: break;

                    case Hand.Rock: {
                        if (hb == Hand.Scissors) {
                            outcome = Outcome.Win;
                        } else if (hb == Hand.Paper) {
                            outcome = Outcome.Loss;
                        }
                    } break;

                    case Hand.Paper: {
                        if (hb == Hand.Rock) {
                            outcome = Outcome.Win;
                        } else if (hb == Hand.Scissors) {
                            outcome = Outcome.Loss;
                        }
                    } break;

                    case Hand.Scissors: {
                        if (hb == Hand.Paper) {
                            outcome = Outcome.Win;
                        } else if (hb == Hand.Rock) {
                            outcome = Outcome.Loss;
                        }
                    } break;
                }
            } else {
                outcome = Outcome.Tie;
            }

            return outcome;
        }

        public static Decision Win(Decision decision, int influence) {
            Hand hand = Hand.NotSpecified;

            switch (decision.MostInfluencedHand) {
                default: break;

                case Hand.Rock: {
                    hand = Hand.Paper;
                } break;

                case Hand.Paper: {
                    hand = Hand.Scissors;
                } break;

                case Hand.Scissors: {
                    hand = Hand.Rock;
                } break;
            }

            return Decision.Distribute(
                hand, influence);
        }

        public static Decision Loss(Decision decision, int influence) {
            Hand hand = Hand.NotSpecified;

            switch (decision.MostInfluencedHand) {
                default: break;

                case Hand.Rock: {
                    hand = Hand.Scissors;
                } break;

                case Hand.Paper: {
                    hand = Hand.Rock;
                } break;

                case Hand.Scissors: {
                    hand = Hand.Paper;
                } break;
            }

            if (hand != Hand.NotSpecified) {
                return Decision.Distribute(
                    hand, influence);
            }

            return Decision.Undecided;
        }

        public static Decision Distribute(int influenceTowardsRock, int influenceTowardsPaper, int influenceTowardsScissors) {
            Decision weights = new Decision() {
                InfluenceTowardsRock = influenceTowardsRock,
                InfluenceTowardsPaper = influenceTowardsPaper,
                InfluenceTowardsScissors = influenceTowardsScissors
            };

            return weights;
        }

        public static Decision Distribute(Hand hands, int influence) {
            if (hands == Hand.NotSpecified) {
                return Decision.Undecided;
            }

            Decision decision = new Decision();

            int modes = (int)hands;

            if ((modes & (int)Hand.Rock) == (int)Hand.Rock) {
                decision.InfluenceTowardsRock += influence;
            }

            if ((modes & (int)Hand.Paper) == (int)Hand.Paper) {
                decision.InfluenceTowardsPaper += influence;
            }

            if ((modes & (int)Hand.Scissors) == (int)Hand.Scissors) {
                decision.InfluenceTowardsScissors += influence;
            }

            return decision;
        }

        public static Decision Next(int influence) {
            Hand hands =
                Hand.Rock |
                Hand.Paper |
                Hand.Scissors;

            return Decision.Next(
                hands, influence);
        }

        public static Decision Next(Hand hands, int influence) {
            if (hands == Hand.NotSpecified) {
                return Decision.Undecided;
            }

            int modes = (int)hands;

            IList<Hand> picks = new List<Hand>();

            if ((modes & (int)Hand.Rock) == (int)Hand.Rock) {
                picks.Add(Hand.Rock);
            }

            if ((modes & (int)Hand.Paper) == (int)Hand.Paper) {
                picks.Add(Hand.Paper);
            }

            if ((modes & (int)Hand.Scissors) == (int)Hand.Scissors) {
                picks.Add(Hand.Scissors);
            }

            Hand hand = picks[
                Roll.Next(0, picks.Count)];

            return Decision.Distribute(
                hand, influence);
        }

        public static Decision operator +(Decision left, Decision right) {
            return Decision.Distribute(
                left.InfluenceTowardsRock + right.InfluenceTowardsRock,
                left.InfluenceTowardsPaper + right.InfluenceTowardsPaper,
                left.InfluenceTowardsScissors + right.InfluenceTowardsScissors);
        }

        public override string ToString() {
            return String.Format("{{ {0}, {1}, {2} }}",
                InfluenceTowardsRock, 
                InfluenceTowardsPaper, 
                InfluenceTowardsScissors);
        }
    }
}
