using System.Collections.Generic;
using NukoWar.Scriptable;
using NukoWar.Scripts.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NukoWar.Scripts.View
{
    public class StageSelectView : MonoBehaviour
    {
        public TextMeshProUGUI stageNameText;
        public StageButton stageButtonPrefab;
        public ScrollRect scrollRect;
        public Transform content;
        
        public List<StageButton> stageButtons = new List<StageButton>();

        private void Start()
        {
            var data = DataManager.Instance.data;
            SetStageNameText(data.currentStage);
            
            // ステージの種類分ボタンを生成する
            for (var i = 0; i < data.allStage.Count; i++)
            {
                var index = i;
                var stageButton = Instantiate(stageButtonPrefab, content);
                stageButton.SetStageNameText((i + 1).ToString());
                stageButtons.Add(stageButton);
                stageButton.GetComponent<Button>().onClick.AddListener(() => SetCurrentStage(data.allStage[index]));

                if (data.allStage[i] == data.currentStage)
                {
                    scrollRect.verticalNormalizedPosition = 1.0f;
                }
            }
        }
        
        private void SetCurrentStage(StageScriptable stage)
        {
            DataManager.Instance.data.currentStage = stage;
            SetStageNameText(stage);
        }
        
        private void SetStageNameText(StageScriptable stage)
        {
            stageNameText.text = stage.stageName;
        }
    }
}
