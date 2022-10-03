using System;
using System.Collections.Generic;

namespace EuroDiffusion_2
{
    public class Diffusion
    {
        public City[,] _cityMap;
        public int _ex_x = 0;
        public int _ex_y = 0;
        public int _currentDay = 0;

        public Diffusion() { }

        public void doSimulation(caseStuct cs)
        {
            int days = 1;
            int diffMoney;
            int cnt;

            while (true)
            {
                if (cs.countryQuantity == 1)
                {
                    cs.toPrint[0].country = cs.countryInclude[0].name;
                    cs.toPrint[0].days = 0;
                    return;
                }

                for (int i = 0; i < _ex_x; i++)
                    for (int j = 0; j < _ex_y; j++)
                        if (_cityMap[i, j] != null)
                            for (int k = 0; k < cs.countryQuantity; k++)
                            {
                                diffMoney = _cityMap[i, j]._wallet._ownWallet[k] / 1000;
                                foreach (var neineighbor in _cityMap[i, j]._neighbors)
                                    neineighbor._wallet._tmpWallet[k] += diffMoney;
                                _cityMap[i, j]._wallet._ownWallet[k] -= diffMoney * _cityMap[i, j]._neighborsCount;
                            }

                for (int i = 0; i < _ex_x; i++)
                    for (int j = 0; j < _ex_y; j++)
                        if (_cityMap[i, j] != null)
                            for (int k = 0; k < _cityMap[i, j]._wallet._tmpWallet.Length; k++)
                            {
                                _cityMap[i, j]._wallet._ownWallet[k] += _cityMap[i, j]._wallet._tmpWallet[k];
                                _cityMap[i, j]._wallet._tmpWallet[k] = 0;
                            }

                for (int i = 0; i < _ex_x; i++)
                    for (int j = 0; j < _ex_y; j++)
                        if (_cityMap[i, j] != null)
                        {
                            cnt = 0;
                            for (int k = 0; k < 20; k++)
                                if (_cityMap[i, j]._wallet._ownWallet[k] != 0)
                                    cnt++;
                            if (cnt == cs.countryQuantity)
                                _cityMap[i, j]._complete = true;
                        }

                for (int i = 0; i < _ex_x; i++)
                    for (int j = 0; j < _ex_y; j++)
                        if (_cityMap[i, j] != null)
                            if (_cityMap[i, j]._complete == true && _cityMap[i, j]._mark != 1)
                            {
                                _cityMap[i, j]._mark = 1;
                                cs.countryInclude[_cityMap[i, j]._countryIndex].townQuantity -= 1;
                            }

                for (int i = 0; i < cs.countryQuantity; i++)
                    if (cs.countryInclude[i].townQuantity == 0 && cs.countryInclude[i].countryComplete != true)
                    {
                        cs.countryInclude[i].countryComplete = true;
                        cs.countryInclude[i].finalDay = days;
                        cs.toPrint[i].country = cs.countryInclude[i].name;
                        cs.toPrint[i].days = cs.countryInclude[i].finalDay;
                    }

                cnt = 0;
                for (int i = 0; i < cs.countryQuantity; i++)
                    if (cs.countryInclude[i].countryComplete == true)
                        cnt++;

                if (cnt == cs.countryQuantity)
                    return;
                days++;
            }
        }

        public void registerNeighbors(caseStuct elem)
        {
            int neighborsCount;
            for (int i = 0; i < _ex_x; i++)
                for (int j = 0; j < _ex_y; j++)
                    if (_cityMap[i, j] != null)
                    {
                        neighborsCount = 0;
                        _cityMap[i, j]._neighbors = new List<City>();
                        if (i != 0 && _cityMap[i - 1, j] != null)
                        {
                            _cityMap[i, j]._neighbors.Add(_cityMap[i - 1, j]);
                            neighborsCount++;
                        }
                        if (j != 0 && _cityMap[i, j - 1] != null)
                        {
                            _cityMap[i, j]._neighbors.Add(_cityMap[i, j - 1]);
                            neighborsCount++;
                        }
                        if (i != (_ex_x - 1) && _cityMap[i + 1, j] != null)
                        {
                            _cityMap[i, j]._neighbors.Add(_cityMap[i + 1, j]);
                            neighborsCount++;
                        }
                        if (j != (_ex_y - 1) && _cityMap[i, j + 1] != null)
                        {
                            _cityMap[i, j]._neighbors.Add(_cityMap[i, j + 1]);
                            neighborsCount++;
                        }
                        _cityMap[i, j]._neighborsCount = neighborsCount;
                        if (neighborsCount == 0)
                        {
                            Console.WriteLine("Programm Error!");
                            return ;
                        }
                    }
        }

        public void registerCountries(caseStuct elem)
        {
            _cityMap = new City[_ex_x, _ex_y];
            int counter = 0;
            foreach (var el in elem.countryInclude)
            {
                for (int i = el.coord[0] - 1; i < el.coord[2]; i++)
                    for (int j = el.coord[1] - 1; j < el.coord[3]; j++)
                    {
                        _cityMap[i, j] = new City(elem, counter, i, j);
                    }
                counter++;
            }
        }

        public void startDiffusion(caseStuct elem)
        {
            foreach (var el in elem.countryInclude)
            {
                if (el.coord[2] > _ex_x) { _ex_x = el.coord[2]; }
                if (el.coord[3] > _ex_y) { _ex_y = el.coord[3]; }
            }
            registerCountries(elem);
            registerNeighbors(elem);
            doSimulation(elem);
        }
    }
}
