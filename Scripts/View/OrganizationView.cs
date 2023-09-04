using System.Collections.Generic;
using NukoWar.Scriptable;
using NukoWar.Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace NukoWar.Scripts.View
{
    public class OrganizationView : MonoBehaviour
    {
        private DataScriptable _data;
        public CardInfoButton cardInfoButtonPrefab;

        public CardInfoButton[] selectedCardInfoButtons = new CardInfoButton[5];
        public List<CardInfoButton> allCardInfoButtons = new List<CardInfoButton>();
        
        public Transform allCardInfoButtonParent;

        private void Start()
        {
            _data = DataManager.Instance.data;
            
            foreach (var unit in _data.allUnits)
            {
                var button = Instantiate(cardInfoButtonPrefab, allCardInfoButtonParent);
                button.SetCardInfo(unit);
                button.GetComponent<Button>().onClick.AddListener(() => Select(unit));
                allCardInfoButtons.Add(button);
            }

            for (var i = 0; i < selectedCardInfoButtons.Length; i++)
            {
                var button = selectedCardInfoButtons[i];
                var index = i;
                button.GetComponent<Button>().onClick.AddListener(() => Clear(index));
            }

            for (var i = 0; i < _data.selectedUnits.Length; i++)
            {
                var selectedUnit = _data.selectedUnits[i];
                if (selectedUnit != null)
                {
                    selectedCardInfoButtons[i].SetCardInfo(selectedUnit);
                }
                else
                {
                    selectedCardInfoButtons[i].Clear();
                }
            }
        }
        
        private void Clear(int index)
        {
            selectedCardInfoButtons[index].Clear();
            _data.selectedUnits[index] = null;
        }

        private void Select(UnitScriptable unitScriptable)
        {
            for (var index = 0; index < selectedCardInfoButtons.Length; index++)
            {
                var selectedCardInfoButton = selectedCardInfoButtons[index];
                //selectedCardInfoButtonがnullではない場合はcontinue
                if (selectedCardInfoButton.CheckCard()) continue;
                //nullの場合はそのボタンに設定してbreak
                selectedCardInfoButton.SetCardInfo(unitScriptable);
                _data.selectedUnits[index] = unitScriptable;
                break;
            }
        }
    }
}
