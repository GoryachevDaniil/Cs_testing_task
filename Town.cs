using System;
using System.Collections.Generic;

namespace EuroDiffusion_2
{
    public class Town
    {
        public string _countryName;
        public int _countryIndex;
        public bool _complete;
        public Wallet _wallet;
        public int _neighborsCount;
        public List<Town> _neighbors;

        public Town(caseStuct elem, int country_index, int x, int y)
        {
            _countryIndex = country_index;
            _wallet = new Wallet(elem, _countryIndex);
            _countryName = elem.countryInclude[country_index].name;
        }
    }
}
