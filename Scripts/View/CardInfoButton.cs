using NukoWar.Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NukoWar.Scripts.View
{
    public class CardInfoButton : MonoBehaviour
    {
        private UnitScriptable _card;
        public TextMeshProUGUI unitNameText;
        public Image cardImage;
        public TextMeshProUGUI costText;
        
        public void SetCardInfo(UnitScriptable card)
        {
            _card = card;
            unitNameText.text = card.unitName;
            cardImage.sprite = card.cardSprite;
            costText.text = Utils.StringUtils.GetWithCurrencyUnit(card.cost);
        }

        public bool CheckCard()
        {
            return _card != null;
        }

        public void Clear()
        {
            _card = null;
            unitNameText.text = "";
            cardImage.sprite = null;
            costText.text = "";
        }
    }
}
