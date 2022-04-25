using System;
using System.Collections.Generic;

namespace EuroDiffusion_2
{
    // Структуры
    public struct caseStuct
    {
        public int countryQuantity;
        public countryStruct[] countryInclude;
        public printStruct[] toPrint;
    }

    public struct countryStruct
    {
        public string name;
        public int[] coord;
        public int townQuantity;
        public List<City> cities;
        public bool countryComplete;
        public int finalDay;
    }

    public struct printStruct
    {
        public string country;
        public int days;
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
            ///
            Diffusion diffusion = new Diffusion();

            foreach (var cs in cases)
            {
                if (cs.countryQuantity == 0) { break; }
                diffusion.startDiffusion(cs);
            }

            //// PRINT RESULT

            int i;
            foreach (var elem in cases)
            {
                i = 1;
                Console.WriteLine($"Case number{i}");
                for (int j = 0; j < elem.countryQuantity; j++)
                    Console.WriteLine($"{elem.toPrint[j].country} {elem.toPrint[j].days}");
                i++;
            }

            Console.WriteLine("End Programm!");
        }
    }
}
