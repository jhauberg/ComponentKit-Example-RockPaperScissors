using System;
using System.Collections.Generic;
using System.Linq;
using ComponentKit;
using ComponentKit.Model;
using ComponentKit.Examples.RockPaperScissors.Behaviors;

namespace ComponentKit.Examples.RockPaperScissors {
    class Program {
        static void Main(string[] args) {
            CreateDefinitions();
            
            using (Simulation sim = new Simulation()) {
                IEntityRecord a = Entity.CreateFromDefinition("Peon", "David");
                IEntityRecord b = Entity.CreateFromDefinition("Brute", "Goliath");

                AddRandomBehaviors(a, maxBehaviors: 3);

                EntityRegistry.Current.Synchronize();

                sim.Run(a, b);
            }

            Console.ReadKey();
        }

        static void CreateDefinitions() {
            Entity.Define("Agent",
                typeof(History),
                typeof(Instinct));

            Entity.Define("Peon", "Agent",
                typeof(Influential<Gifted>));

            Entity.Define("Brute", "Agent",
                typeof(Unpredictable),
                typeof(Brute),
                typeof(Influential<Brute>));
        }

        static void AddRandomBehaviors(IEntityRecord agent, int maxBehaviors) {
            IList<Type> behaviors = 
                new List<Type>();

            behaviors.Add(typeof(Brute));
            behaviors.Add(typeof(Cheating));
            behaviors.Add(typeof(Gifted));
            behaviors.Add(typeof(Paranoid));
            behaviors.Add(typeof(Predictable));
            behaviors.Add(typeof(Stubborn));
            behaviors.Add(typeof(Unpredictable));

            int attachedBehaviors = 0;

            int retries = 0;
            int maxRetries = 4;

            while (attachedBehaviors < maxBehaviors) {
                Type behaviorType = behaviors[Roll.Next(0, behaviors.Count)];
                IComponent behavior = Component.Create(behaviorType);

                if (agent.Add(behavior)) {
                    attachedBehaviors++;
                }

                if (retries++ > maxRetries) {
                    break;
                }
            }
        }
    }
}

///Copyright 2012 Jacob H. Hansen.