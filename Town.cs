using System;
using System.Collections.Generic;

namespace EuroDiffusion_2
{
    public class Town
    {
        // Дата
        public string _countryName;
        public int _countryIndex;
        public bool _complete;
        public Wallet _wallet;
        public int _neighborsCount;
        //public int[,] _neighborsCoors;
        public List<Town> _neighbors;


        // Конструктор
        public Town(caseStuct elem, int country_index, int x, int y)
        {
            _countryIndex = country_index;
            _wallet = new Wallet(elem, _countryIndex);
            _countryName = elem.countryInclude[country_index].name;
            //_neighborsCoors = new int[4, 2];
        }

        // Методы
    }
}
