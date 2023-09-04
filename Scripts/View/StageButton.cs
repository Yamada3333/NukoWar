using TMPro;
using UnityEngine;

namespace NukoWar.Scripts.View
{
    public class StageButton : MonoBehaviour
    {
        public TextMeshProUGUI stageNameText;
        
        public void SetStageNameText(string stageName)
        {
            stageNameText.text = stageName;
        }
    }
}
