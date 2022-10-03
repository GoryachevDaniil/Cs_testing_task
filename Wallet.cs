namespace EuroDiffusion_2
{
    public class Wallet
    {
        public int[] _ownWallet;
        public int[] _tmpWallet;

        public Wallet(caseStuct elem, int country_index)
        {
            _ownWallet = new int[20];
            _tmpWallet = new int[20];
            for (int i = 0; i < elem.countryQuantity; i++)
                _ownWallet[i] = (i == country_index ? 1000000 : 0);
            for (int i = 0; i < elem.countryQuantity; i++)
                _tmpWallet[i] = 0;
        }
    }
}
