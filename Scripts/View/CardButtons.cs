using NukoWar.Scripts.Manager;
using UnityEngine;

namespace NukoWar.Scripts.View
{
    public class CardButtons : MonoBehaviour
    {
        public CardButton cardButtonPrefab;

        private void Start()
        {
            // ユニットの種類分ボタンを生成する
            foreach (var card in DataManager.Instance.data.selectedUnits)
            {
                if (card == null) continue;
                var cardButton = Instantiate(cardButtonPrefab, transform);
                cardButton.SetCard(card);
            }
        }
    }
}
