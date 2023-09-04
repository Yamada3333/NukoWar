using NukoWar.Scripts.Enum;
using NukoWar.Scripts.Interface;
using NukoWar.Scripts.Manager;
using NukoWar.Scripts.Utils;
using UniRx;
using UnityEngine;

namespace NukoWar.Scripts.Unit
{
    public class ColliderCheck : MonoBehaviour
    {
        public readonly Subject<ITarget> onHit = new();
        public CircleCollider2D circleCollider2D;

        private void Start()
        {
            if (circleCollider2D == null) circleCollider2D = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (GameModeManager.Instance.CurrentGameMode != GameMode.Play) return;
            var unit = CheckTarget(collision);
            if (unit != null) onHit.OnNext(unit);
        }
        
        /// <summary>
        /// ITargetを持っているかつ敵の場合のみ返す
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        private ITarget CheckTarget(Component other)
        {
            if(!other.TryGetComponent(out ITarget target)) return null;
            return other.gameObject.layer != LayerUtils.GetEnemyLayer(gameObject.layer) ? null : target;
        }
    }
}
