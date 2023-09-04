using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NukoWar.Scripts.View
{
    public class SceneChangeButton : MonoBehaviour
    {
        public string sceneName;
        
        public void Start()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }
        
        private void OnClick()
        {
            //sceneNameのシーンをロードする
            SceneManager.LoadScene(sceneName);
        }
    }
}
