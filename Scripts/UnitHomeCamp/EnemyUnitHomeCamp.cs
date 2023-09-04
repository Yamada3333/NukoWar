using NukoWar.Scriptable;
using NukoWar.Scripts.Enum;
using NukoWar.Scripts.Manager;
using UniRx;

namespace NukoWar.Scripts.UnitHomeCamp
{
    public class EnemyUnitHomeCamp : UnitHomeCamp
    {
        private float _timer;
        private float _spawnTime;
        private int _faveCount;
        private StageScriptable _stageScriptable;

        private void Start()
        {
            TimeManager.Instance.TimeSubject
                //Playの時のみ更新する
                .Where(_ => GameModeManager.Instance.CurrentGameMode == GameMode.Play)
                //時間経過で所持金を増やす
                .Subscribe(OnUpdate).AddTo(gameObject);
            _stageScriptable = DataManager.Instance.data.currentStage;
            _faveCount = 0;
            SpawnUnit();
        }

        private void OnUpdate(float time)
        {
            _timer += time;
            //一定時間経過で敵を生成する
            if (!(_timer >= _spawnTime)) return;
            SpawnUnit();
        }

        private void SpawnUnit()
        {
            var fave = _stageScriptable.faves.Count > _faveCount
                ? _stageScriptable.faves[_faveCount]
                : _stageScriptable.unlimitedFave;
            
            foreach (var unit in fave.units)
            {
                SpawnUnit(unit, true);
            }
            
            //タイマーの初期化も行う
            _timer = 0;
            _faveCount++;
            SetSpawnTime();
        }

        private void SetSpawnTime()
        {
            var spawnTime = _stageScriptable.faves.Count > _faveCount
                ? _stageScriptable.faves[_faveCount].spawnTime
                : _stageScriptable.unlimitedFave.spawnTime;

            _spawnTime = spawnTime;
        }
    }
}
