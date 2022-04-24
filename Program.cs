using System;
using System.Collections.Generic;

namespace EuroDiffusion_2
{
    // Структуры
    public struct caseStuct
    {
        public int countryQuantity;
        public countryStruct[] countryInclude;
    }

    public struct countryStruct
    {
        public string name;
        public int[] coord;
        public int townQuantity;
        public List<Town> towns;
        public bool countryComplete;
        public int finalDay;
        //public int case_number; // ?
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            List<caseStuct> cases = new List<caseStuct>();

            Parser parser = new Parser();

            // Парсим входные данные

            int parser_result;
            while (true)
            {
                parser_result = parser.start_parse(cases);
                if (parser_result == 0)
                {
                    Console.WriteLine("Programm Error!");
                    return;
                }
                if (parser_result == 2) { break; }
            }

            //// Отрабатываем симуляцию для каждого кейса
            Simulation simulation = new Simulation();

            foreach (var cs in cases)
            {
                if (cs.countryQuantity == 0) { break; }
                simulation.startSimulation(cs);
            }

            //// PRINT RESULT

            int i;
            foreach (var elem in cases)
            {
                i = 1;
                Console.WriteLine($"Case number{i}");
                for (int j = 0; j < elem.countryQuantity; j++)
                {
                    Console.WriteLine($"{elem.countryInclude[j].name} {elem.countryInclude[j].finalDay}");
                }
                i++;
            }

            Console.WriteLine("End Programm!");
        }
    }
}
