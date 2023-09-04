using TMPro;
using UnityEngine;

namespace NukoWar.Scripts.View
{
    public class TitleButton : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshProUgui;
        
        private void Start()
        {
            gameObject.SetActive(false);
            _textMeshProUgui = GetComponentInChildren<TextMeshProUGUI>();
        }
        
        public void SetText(string text)
        {
            gameObject.SetActive(true);
            _textMeshProUgui.text = text;
        }
    }
}
