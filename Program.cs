using System;
using System.Collections.Generic;

namespace EuroDiffusion_2
{
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

            Diffusion diffusion = new Diffusion();

            foreach (var cs in cases)
            {
                if (cs.countryQuantity == 0) { break; }
                diffusion.startDiffusion(cs);
            }

            int i = 1;
            foreach (var cs in cases)
            {
                Console.WriteLine($"Case Number {i}");
                printStruct[] tmp = new printStruct[cs.countryQuantity];
                tmp = BubbleSort(cs.toPrint);
                foreach (var t in tmp)
                {
                    Console.WriteLine($"    {t.country} {t.days}");
                }
                i++;
            }

            Console.WriteLine("End Programm!");
        }

        static printStruct[] BubbleSort(printStruct[] mas)
        {
            printStruct temp;
            for (int i = 0; i < mas.Length; i++)
            {
                for (int j = i + 1; j < mas.Length; j++)
                {
                    if (mas[i].days > mas[j].days)
                    {
                        temp = mas[i];
                        mas[i] = mas[j];
                        mas[j] = temp;
                    }
                }
            }
            return mas;
        }
    }
}
