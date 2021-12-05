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

        private static int TestedDataAmount = 50;
        
        private static string initialSeparation = "InitialSeparation";
        private static string overtakingSpeed = "OvertakingSpeed";
        private static string oncomingSpeed = "OncomingSpeed";
        private static string overtake = "Overtake";

        private static string initialSeparationData { get; set; }
        private static string overtakingSpeedData { get; set; }
        private static string oncomingSpeedData { get; set; }
        private static string overtakeData { get; set; }
        private static int CorrectAns { get; set; }
        private static double Percentage { get; set; }
        private static int rndDataType { get; set; }


        private static RuleEngine ruleEngine = new RuleEngine();
        public Model()
        {
            ReadData();
            
        }

        private static void AddFacts(string type)
        {
            if(type == "AllData")
            {
                ruleEngine.ClearFacts();
                ruleEngine.AddFact(new IsClause(initialSeparation, initialSeparationData));
                ruleEngine.AddFact(new IsClause(overtakingSpeed, overtakingSpeedData));
                ruleEngine.AddFact(new IsClause(oncomingSpeed, oncomingSpeedData));
            }
            else if (type == "TwoData")
            {
                ruleEngine.ClearFacts();
                rndDataType = rnd.Next(1 , 3);

                if(rndDataType == 1)
                {
                    ruleEngine.AddFact(new IsClause(initialSeparation, initialSeparationData));
                    ruleEngine.AddFact(new IsClause(overtakingSpeed, overtakingSpeedData));
                }
                else if (rndDataType == 2)
                {
                    ruleEngine.AddFact(new IsClause(overtakingSpeed, overtakingSpeedData));
                    ruleEngine.AddFact(new IsClause(oncomingSpeed, oncomingSpeedData));
                }
                else
                {
                    ruleEngine.AddFact(new IsClause(initialSeparation, initialSeparationData));
                    ruleEngine.AddFact(new IsClause(oncomingSpeed, oncomingSpeedData));
                }
            }
        }

        private static void ForwardChain()
        {
            ruleEngine.Infer();
            Console.WriteLine("after inference");
            foreach ( var fact in ruleEngine.Facts.facts)
            {
                Console.WriteLine(fact);
            }

            if(ruleEngine.Facts.facts.Count == 4)
            {
                PercentageCount(Convert.ToString(ruleEngine.Facts.facts[3]), overtakeData);
            }
            else
            {
                PercentageCount("False", overtakeData);
            }
        }

        private static void BackwardChain()
        {
            Console.WriteLine("\nInfer: Overtake");
            var conclusion = ruleEngine.Infer("Overtake");
            Console.WriteLine("Conclusion: " + conclusion);
        }

        private static void PercentageCount(string TestedDataAns, string DataAns)
        {
            if (TestedDataAns == "Overtake = " + DataAns)
            {
                CorrectAns++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correct answer");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("incorrect answer");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Percentage = (100 / TestedDataAmount) * CorrectAns;
        }

        private static void AddRules()
        {
            rule = new Rule(overtakeData);
            rule.AddAntecedent(new IsClause(initialSeparation, initialSeparationData));
            rule.AddAntecedent(new IsClause(overtakingSpeed, overtakingSpeedData));
            rule.AddAntecedent(new IsClause(oncomingSpeed, oncomingSpeedData));
            rule.setConsequent(new IsClause(overtake, overtakeData));
            ruleEngine.AddRule(rule);
        }

        private static void ReadData()
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

            int colmn;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Existing Data");
            Console.ForegroundColor = ConsoleColor.White;
            for (int times = 1; times < TestedDataAmount+1; times++)
            {
                colmn = rnd.Next(0, allInputs.GetLength(0));

                initialSeparationData = $"{float.Parse(allInputs[colmn][0]):F1}";
                overtakingSpeedData = $"{float.Parse(allInputs[colmn][1]):F1}";
                oncomingSpeedData = $"{float.Parse(allInputs[colmn][2]):F1}";
                overtakeData = allInputs[colmn][3];

                Console.WriteLine($"\nTested Data : {times}");
                AddFacts("AllData");
                ForwardChain();
                Console.WriteLine($"Correct Answer: {overtakeData}");
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nPercentage : {Percentage}\n");
            Console.ForegroundColor = ConsoleColor.White;

            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine("Existing Data");
            //Console.ForegroundColor = ConsoleColor.White;
            //CorrectAns = 0;
            //Percentage = 0;

            //for (int times = 0; times < TestedDataAmount; times++)
            //{
            //    colmn = rnd.Next(0, allInputs.GetLength(0));


            //    initialSeparationData = $"{float.Parse(allInputs[colmn][0]):F1}";
            //    overtakingSpeedData = $"{float.Parse(allInputs[colmn][1]):F1}";
            //    oncomingSpeedData = $"{float.Parse(allInputs[colmn][2]):F1}";
            //    overtakeData = allInputs[colmn][3];

            //    Console.WriteLine($"\nTested Data : {times}");
            //    AddFacts("TwoData");
            //    ForwardChain();
            //    Console.WriteLine($"Correct Answer: {overtakeData}");
            //}

            //Console.ForegroundColor = ConsoleColor.Magenta;
            //Console.WriteLine($"Percentage : {Percentage}\n");
            //Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
