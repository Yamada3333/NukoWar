using UniRx;
using UnityEngine;

namespace NukoWar.Scripts.Manager
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }
        public Subject<float> TimeSubject { get; } = new();
        
        private void Awake()
        {
            Instance = this;
        }
        
        private void Update()
        {
            TimeSubject.OnNext(Time.deltaTime);
        }
    }
}
