using NukoWar.Scripts.Interface;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace NukoWar.Scripts.View
{
    public class HealthSlider : MonoBehaviour
    {
        public Slider slider;
        public GameObject target;
        private ITarget _target;

        private void Start()
        {
            if (target.TryGetComponent(out ITarget iTarget))
            {
                _target = iTarget;
                slider.maxValue = _target.GetMaxHealth();
                _target.CurrentHealth.Subscribe(ChangeHealth);
            }
            else
            {
                Debug.LogError("ITargetを実装していません");
                gameObject.SetActive(false);
            }
        }
        
        private void ChangeHealth(int health)
        {
            slider.value = health;
        }
    }
}
