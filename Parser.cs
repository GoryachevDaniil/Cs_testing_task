using System;
using System.Collections.Generic;

namespace EuroDiffusion_2
{
    public class Parser
    {
        // Data
        public string[] _line;
        public int _country_quantity;

        // Constructor
        public Parser() { }

        // Methods
        public int start_parse(List<caseStuct> cases)
        {
            caseStuct cs = new caseStuct();
            if (!int.TryParse(Console.ReadLine(), out _country_quantity)) { return 0; }
            if (_country_quantity == 0) { return 2; }
            cs.countryInclude = new countryStruct[_country_quantity];
            cs.toPrint = new printStruct[_country_quantity];
            for (int i = 0; i < _country_quantity; i++)
            {
                _line = Console.ReadLine().Split(new char[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries);
                if (_line.Length != 5) { return 0; }
                cs.countryInclude[i].coord = new int[4];
                cs.countryQuantity = _country_quantity;
                cs.countryInclude[i].name = _line[0];
                cs.countryInclude[i].coord[0] = Convert.ToInt32(_line[1]);
                cs.countryInclude[i].coord[1] = Convert.ToInt32(_line[2]);
                cs.countryInclude[i].coord[2] = Convert.ToInt32(_line[3]);
                cs.countryInclude[i].coord[3] = Convert.ToInt32(_line[4]);
                cs.countryInclude[i].townQuantity = ((cs.countryInclude[i].coord[2]
                    - cs.countryInclude[i].coord[0]) + 1) * ((cs.countryInclude[i].coord[3]
                    - cs.countryInclude[i].coord[1]) + 1);
                cs.countryInclude[i].countryComplete = false;
            }
            cases.Add(cs);
            return 1;
        }
    }
}
