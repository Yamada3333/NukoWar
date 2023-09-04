using NukoWar.Scripts.Manager;

namespace NukoWar.Scripts.Utils
{
    public static class StringUtils
    {
        /// <summary>
        /// 通貨単位を付けて金額を返す
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string GetWithCurrencyUnit(int money)
        {
            var currencyUnit = DataManager.Instance.data.currencyUnit;
            return $"{money}{currencyUnit}";
        }
    }
}
