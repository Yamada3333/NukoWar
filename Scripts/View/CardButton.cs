using NukoWar.Scriptable;
using NukoWar.Scripts.Player;
using NukoWar.Scripts.UnitHomeCamp;
using NukoWar.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace NukoWar.Scripts.View
{
    public class CardButton : MonoBehaviour
    {
        PlayerMoney _playerMoney;
        PlayerUnitHomeCamp _playerUnitHomeCamp;
        [SerializeField] private Image image;
        [SerializeField] private TMPro.TextMeshProUGUI costText;
        private int _cost;
        private UnitScriptable _card;
        
        public void SetCard(UnitScriptable card)
        {
            _card = card;
            image.sprite = card.cardSprite;
            _cost = card.cost;
            costText.text = StringUtils.GetWithCurrencyUnit(_cost);
            
            _playerMoney = FindObjectOfType<PlayerMoney>();
            _playerMoney.CurrentMoney.Subscribe(SetInteractable);
            _playerUnitHomeCamp = FindObjectOfType<PlayerUnitHomeCamp>();
        }

        public void OnClick()
        {
            _playerMoney.ReduceCurrentMoney(_cost);
            _playerUnitHomeCamp.SpawnUnit(_card, false);
        }
        
        /// <summary>
        /// 現在の所持金に応じてボタンの有効無効を切り替える
        /// </summary>
        /// <param name="money"></param>
        private void SetInteractable(int money)
        {
            GetComponent<Button>().interactable = money >= _cost;
        }
    }
}
