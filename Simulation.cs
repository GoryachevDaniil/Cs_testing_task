using System;
using System.Collections.Generic;

namespace EuroDiffusion_2
{
    public class Simulation
    {
        public Town[,] _townMap;
        public int?[,] _map;
        public int _ex_x = 0;
        public int _ex_y = 0;
        public int currentDay = 0;

        public Simulation() { }

        public void doSimulation(caseStuct cs)
        {
            int days = 1;
            int diffMoney = 0;
            while (true)
            {
                if (cs.countryQuantity == 1)
                    days = 0;
                for (int i = 0; i < _ex_x; i++)
                    for (int j = 0; j < _ex_y; j++)
                        if (_townMap[i, j] != null)
                            for (int k = 0; k < cs.countryQuantity; k++)
                                if (_townMap[i, j]._wallet._ownWallet[k] > 1000)
                                {
                                    diffMoney = _townMap[i, j]._wallet._ownWallet[k] / 1000;
                                    foreach (var neineighbor in _townMap[i, j]._neighbors)
                                        neineighbor._wallet._tmpWallet[k] += diffMoney;
                                    _townMap[i, j]._wallet._ownWallet[k] -= diffMoney * _townMap[i, j]._neighborsCount;
                                }

                for (int i = 0; i < _ex_x; i++)
                    for (int j = 0; j < _ex_y; j++)
                        if (_townMap[i, j] != null)
                            for (int k = 0; k < _townMap[i, j]._wallet._tmpWallet.Length; k++)
                            {
                                _townMap[i, j]._wallet._ownWallet[k] += _townMap[i, j]._wallet._tmpWallet[k];
                                _townMap[i, j]._wallet._tmpWallet[k] = 0;
                            }

                int completeTown;
                for (int i = 0; i < _ex_x; i++)
                    for (int j = 0; j < _ex_y; j++)
                        if (_townMap[i, j] != null)
                        {
                            completeTown = 0;
                            for (int k = 0; k < _townMap[i, j]._wallet._tmpWallet.Length; k++)
                            {
                                if (_townMap[i, j]._wallet._ownWallet[k] != 0)
                                    completeTown += 1;
                            }
                            if (completeTown == cs.countryQuantity)
                                cs.countryInclude[_townMap[i, j]._countryIndex].townQuantity -= 1;
                        }

                for (int i = 0; i < cs.countryQuantity; i++)
                    if (cs.countryInclude[i].townQuantity == 0)
                    {
                        cs.countryInclude[i].countryComplete = true;
                        cs.countryInclude[i].finalDay = days;
                    }

                bool completeCountry = true;
                for (int i = 0; i < cs.countryQuantity; i++)
                    if (cs.countryInclude[i].countryComplete == false)
                        completeCountry = false;

                if (completeCountry == true)
                    return;
                days++;
            }
        }

        public void registerNeighbors(caseStuct elem)
        {
            int neighborsCount;
            for (int i = 0; i < _ex_x; i++)
                for (int j = 0; j < _ex_y; j++)
                    if (_townMap[i, j] != null)
                    {
                        neighborsCount = 0;
                        _townMap[i, j]._neighbors = new List<Town>();
                        if (i != 0 && _townMap[i - 1, j] != null)
                        {
                            _townMap[i, j]._neighbors.Add(_townMap[i - 1, j]);
                            neighborsCount++;
                        }
                        if (j != 0 && _townMap[i, j - 1] != null)
                        {
                            _townMap[i, j]._neighbors.Add(_townMap[i, j - 1]);
                            neighborsCount++;
                        }
                        if (i != (_ex_x - 1) && _townMap[i + 1, j] != null)
                        {
                            _townMap[i, j]._neighbors.Add(_townMap[i + 1, j]);
                            neighborsCount++;
                        }
                        if (j != (_ex_y - 1) && _townMap[i, j + 1] != null)
                        {
                            _townMap[i, j]._neighbors.Add(_townMap[i, j + 1]);
                            neighborsCount++;
                        }
                        _townMap[i, j]._neighborsCount = neighborsCount;
                    }
        }

        public void registerCountries(caseStuct elem)
        {
            _townMap = new Town[_ex_x, _ex_y];
            int counter = 0;
            foreach (var el in elem.countryInclude)
            {
                for (int i = el.coord[0] - 1; i < el.coord[2]; i++)
                    for (int j = el.coord[1] - 1; j < el.coord[3]; j++)
                    {
                        _townMap[i, j] = new Town(elem, counter, i, j);
                    }
                counter++;
            }
        }

        public void startSimulation(caseStuct elem)
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
