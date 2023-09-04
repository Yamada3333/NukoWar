using NukoWar.Scriptable;
using NukoWar.Scripts.Interface;
using NukoWar.Scripts.Unit;
using UniRx;
using UnityEngine;

namespace NukoWar.Scripts.UnitHomeCamp
{
    public class UnitHomeCamp : MonoBehaviour, ITarget
    {
        public Transform unitSpawnPoint;
        public UnitBase unitPrefab;

        private readonly IntReactiveProperty _currentHealth = new();
        private const int MaxHealth = 100;

        public IReadOnlyReactiveProperty<int> CurrentHealth => _currentHealth;

        private void Awake()
        {
            _currentHealth.Value = MaxHealth;
        }

        public void OnDamage(int damage)
        {
            _currentHealth.Value -= damage;
        }

        public int GetMaxHealth()
        {
            return MaxHealth;
        }


        public void SpawnUnit(UnitScriptable unitScriptable, bool isEnemy)
        {
            var unit = Instantiate(unitPrefab, unitSpawnPoint.position, Quaternion.identity);
            unit.transform.SetParent(unitSpawnPoint);
            unit.SetUnit(unitScriptable, isEnemy);
        }

    }
}
