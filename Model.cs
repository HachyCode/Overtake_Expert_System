using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Overtake_Expert_System
{
    class Model
    {
        private static Rule rule;
        private static Random rnd = new Random();

        private static string initialSeparation = "InitialSeparation";
        private static string overtakingSpeed = "OvertakingSpeed";
        private static string oncomingSpeed = "OncomingSpeed";
        private static string overtake = "Overtake";

        private static string initialSeparationData { get; set; }
        private static string overtakingSpeedData { get; set; }
        private static string oncomingSpeedData { get; set; }
        private static string overtakeData { get; set; }

        private static RuleEngine ruleEngine = new RuleEngine();
        public Model()
        {
            ReadData();

            ForwardChain();
            BackwardChain();
        }

        public static void AddFacts()
        {
            ruleEngine.ClearFacts();
            ruleEngine.AddFact(new IsClause(initialSeparation, initialSeparationData));
            ruleEngine.AddFact(new IsClause(overtakingSpeed, overtakingSpeedData));
            ruleEngine.AddFact(new IsClause(oncomingSpeed, oncomingSpeedData));
        }

        public static void ForwardChain()
        {
            ruleEngine.Infer();
            Console.WriteLine("\nafter inference");
            foreach ( var fact in ruleEngine.Facts.facts)
            {
                Console.WriteLine(fact);
            }
        }

        public static void BackwardChain()
        {
            Console.WriteLine("\nInfer: Overtake");
            var conclusion = ruleEngine.Infer("Overtake");
            Console.WriteLine("Conclusion: " + conclusion);
        }

        public static void AddRules()
        {
            rule = new Rule(overtakeData);
            rule.AddAntecedent(new IsClause(initialSeparation, initialSeparationData));
            rule.AddAntecedent(new IsClause(overtakingSpeed, overtakingSpeedData));
            rule.AddAntecedent(new IsClause(oncomingSpeed, oncomingSpeedData));
            rule.setConsequent(new IsClause(overtake, overtakeData));
            ruleEngine.AddRule(rule);
        }

        public static void ReadData()
        {
            string[] dataset = File.ReadAllLines(@"OvertakeData.csv");
            string[][] allInputs = dataset
                .Select(x => x.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();

            for (int col = 0; col < allInputs.GetLength(0); col++)
            {
                initialSeparationData = $"{float.Parse(allInputs[col][0]):F1}";
                overtakingSpeedData = $"{float.Parse(allInputs[col][1]):F1}";
                oncomingSpeedData = $"{float.Parse(allInputs[col][2]):F1}";
                overtakeData = allInputs[col][3];

                AddRules();
            }

            int colmn = rnd.Next(0, allInputs.GetLength(0));

            initialSeparationData = $"{float.Parse(allInputs[colmn][0]):F1}";
            overtakingSpeedData = $"{float.Parse(allInputs[colmn][1]):F1}";
            oncomingSpeedData = $"{float.Parse(allInputs[colmn][2]):F1}";

            AddFacts();
        }
    }
}
