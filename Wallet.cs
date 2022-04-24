using System;
namespace EuroDiffusion_2
{
    public class Wallet
    {
        // Дата
        public int[] _ownWallet; //  типы монет монет(стран)
        public int[] _tmpWallet; //  типы монет монет(стран)


        // Конструктор
        public Wallet(caseStuct elem, int country_index)
        {
            _ownWallet = new int[20];//elem.countryQuantity 
            _tmpWallet = new int[20];
            for (int i = 0; i < elem.countryQuantity; i++)
                _ownWallet[i] = (i == country_index ? 1000000 : 0);
            for (int i = 0; i < elem.countryQuantity; i++)
                _tmpWallet[i] = 0;
        }

        // Методы
    }
}
