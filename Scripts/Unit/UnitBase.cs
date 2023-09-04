using System;
using DG.Tweening;
using NukoWar.Scriptable;
using NukoWar.Scripts.Enum;
using NukoWar.Scripts.Interface;
using NukoWar.Scripts.Manager;
using NukoWar.Scripts.Utils;
using UniRx;
using UnityEngine;

namespace NukoWar.Scripts.Unit
{
    public class UnitBase : MonoBehaviour ,ITarget ,IAttacker
    {
        [SerializeField] private UnitScriptable unit;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private ColliderCheck colliderCheck;

        private float _timer;
        private ITarget _target;
        private IDisposable _disposable;
        
        private readonly IntReactiveProperty _currentHealth = new();
        public IReadOnlyReactiveProperty<int> CurrentHealth => _currentHealth;


        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            TimeManager.Instance.TimeSubject
                //Playの時のみ更新する
                .Where(_ => GameModeManager.Instance.CurrentGameMode == GameMode.Play)
                .Subscribe(OnUpdate).AddTo(gameObject);
            colliderCheck.onHit.Subscribe(OnStayEnemy).AddTo(gameObject);
        }

        public void SetUnit(UnitScriptable setUnit, bool isEnemy)
        {
            unit = setUnit;
            _currentHealth.Value = unit.hp;
            var layer = LayerUtils.GetUnitLayer(isEnemy);
            //子供のオブジェクトも含めレイヤーを変更する
            gameObject.layer = layer;
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.layer = layer;
            }
            
            spriteRenderer.sprite = setUnit.unitSprite;
            //キャラの被りを防ぐためランダムに少し位置をずらす
            var randomPosition =
                new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f));
            spriteRenderer.transform.localPosition = randomPosition;
            spriteRenderer.transform.rotation = Quaternion.Euler(LayerUtils.GetRotation(gameObject.layer));
            colliderCheck.circleCollider2D.radius = setUnit.attackRange;
        }

        private void OnUpdate(float time)
        {
            _timer += time;
            //攻撃対象がいない場合は移動する
            if (_target == null)
            {
                var force = new Vector2(unit.moveSpeed * LayerUtils.GetDirection(gameObject.layer) * 0.5f, 0.0f);
                _rigidbody2D.velocity = force;
            }
            //攻撃対象がいる場合は攻撃する
            else
            {
                if(_target == null) return;
                if (_timer < unit.attackInterval) return;
                OnAttack(_target);
            }
        }

        public void OnAttack(ITarget target)
        {
            _timer = 0.0f;
            AttackAnimation();
            target.OnDamage(unit.attack);
        }
        
        /// <summary>
        /// 攻撃を受ける
        /// </summary>
        public void OnDamage(int damage)
        {
            _currentHealth.Value -= damage;
            if (_currentHealth.Value > 0) return;
            Destroy(gameObject);
        }
        
        public int GetMaxHealth()
        {
            return unit.hp;
        }

        private void AttackAnimation()
        {
            //unitの方向にDotWeenで移動して0.1秒後に元の位置に戻る
            const float time = 0.1f;
            var direction = new Vector3(LayerUtils.GetDirection(gameObject.layer), 0, 0);
            var spritePosition = spriteRenderer.transform.localPosition;
            //directionの方向に0.01f移動する
            spriteRenderer.transform
                .DOLocalJump(direction * 0.1f, 0.1f, 1, time)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
            //0.1秒後に元の位置に戻る
            spriteRenderer.transform.DOLocalJump(spritePosition, 0.1f, 1, time)
                .SetEase(Ease.Linear)
                .SetDelay(time)
                .SetLink(gameObject);
        }

        private void OnStayEnemy(ITarget target)
        {
            _rigidbody2D.velocity = Vector2.zero;

            //_targetがnullの場合は攻撃対象に設定する
            if (_target != null && target.CurrentHealth.Value <= 0) return;
            _target = target;
            //攻撃対象のHPが0以下になったら攻撃対象をnullにする
            _disposable = _target.CurrentHealth.Subscribe(CheckTargetHealth);
        }

        private void CheckTargetHealth(int targetHealth)
        {
            if (targetHealth > 0) return;
            _target = null;
            _disposable.Dispose();
        }
    }
}
