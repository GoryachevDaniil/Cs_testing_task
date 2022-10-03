using System.Collections.Generic;

namespace EuroDiffusion_2
{
    public class City
    {
        public string _countryName;
        public int _countryIndex;
        public bool _complete;
        public Wallet _wallet;
        public int _neighborsCount;
        public List<City> _neighbors;
        public int _mark = 0;

        public City(caseStuct elem, int country_index, int x, int y)
        {
            _countryIndex = country_index;
            _wallet = new Wallet(elem, _countryIndex);
            _countryName = elem.countryInclude[country_index].name;
            _complete = false;
        }
    }
}
