using NukoWar.Scripts.Enum;
using NukoWar.Scripts.Manager;
using UniRx;
using UnityEngine;

namespace NukoWar.Scripts.Player
{
    public class PlayerMoney : MonoBehaviour
    {
        
        private readonly IntReactiveProperty _maxMoney = new();
        public IReactiveProperty<int> MaxMoney => _maxMoney;
        private readonly IntReactiveProperty _currentMoney = new();
        public IReactiveProperty<int> CurrentMoney => _currentMoney;
        
        private readonly ReactiveProperty<float> _moneyRecoveryTime = new();
        public IReadOnlyReactiveProperty<float> MoneyRecoveryTime => _moneyRecoveryTime;
        private readonly ReactiveProperty<float> _moneyRecoveryTimer = new();
        public IReadOnlyReactiveProperty<float> MoneyRecoveryTimer => _moneyRecoveryTimer;
        
        public int recoveryMoney = 100;

        private void Start()
        {
            var data = DataManager.Instance.data;
            _maxMoney.Value = data.maxMoney;
            _currentMoney.Value = data.currentMoney;
            recoveryMoney = data.recoveryMoney;
            _moneyRecoveryTime.Value = data.moneyRecoveryTime;

            TimeManager.Instance.TimeSubject
                //Playの時のみ更新する
                .Where(_ => GameModeManager.Instance.CurrentGameMode == GameMode.Play)
                //時間経過で所持金を増やす
                .Subscribe(OnUpdate).AddTo(gameObject);
        }

        /// <summary>
        /// 所持金を減らす。
        /// 減らせた場合はtrueを返す。
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public bool ReduceCurrentMoney(int cost)
        {
            //所持金が足りない場合はfalseを返す
            if (_currentMoney.Value < cost) return false;
            //所持金が足りている場合は所持金を減らし、trueを返す
            _currentMoney.Value -= cost;
            return true;
        }

        private void OnUpdate(float time)
        {
            _moneyRecoveryTimer.Value += time;
            if (!(_moneyRecoveryTimer.Value >= _moneyRecoveryTime.Value)) return;
            _moneyRecoveryTimer.Value = 0;
            _currentMoney.Value = Mathf.Min(_currentMoney.Value + recoveryMoney, _maxMoney.Value);
        }
    }
}
