using NukoWar.Scripts.Player;
using NukoWar.Scripts.Utils;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace NukoWar.Scripts.View
{
    public class MoneyText : MonoBehaviour
    {
        [SerializeField] private PlayerMoney playerMoney;
        [SerializeField] private TMPro.TextMeshProUGUI text;
        [SerializeField] private Slider slider;

        private void Start()
        {
            //インスペクターに設定されている場合はそちらを優先する
            if (playerMoney == null) playerMoney = FindObjectOfType<PlayerMoney>();
            playerMoney.CurrentMoney.Subscribe(SetMoneyText);
            playerMoney.MoneyRecoveryTime.Subscribe(SetSliderMaxValue);
            playerMoney.MoneyRecoveryTimer.Subscribe(SetSliderValue);
        }

        private void SetMoneyText(int money)
        {
            text.text = StringUtils.GetWithCurrencyUnit(money);
        }
        
        private void SetSliderMaxValue(float time)
        {
            slider.maxValue = time;
        }
        
        private void SetSliderValue(float time)
        {
            slider.value = time;
        }
    }
}
