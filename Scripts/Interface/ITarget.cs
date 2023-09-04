using UniRx;
using UnityEngine;

namespace NukoWar.Scripts.Interface
{
    public interface ITarget
    {
        public IReadOnlyReactiveProperty<int> CurrentHealth { get;  }
        
        public void OnDamage(int damage);
        public int GetMaxHealth();
    }
}
