using UnityEngine;

namespace NukoWar.Scripts.Utils
{
    public static class LayerUtils 
    {
        /// <summary>
        /// プレイヤーと敵のレイヤーを切り替える
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static int GetEnemyLayer(int layer)
        {
            return layer == LayerMask.NameToLayer("Player")
                ? LayerMask.NameToLayer("Enemy")
                : LayerMask.NameToLayer("Player");
        }

        /// <summary>
        /// 自身のレイヤーを取得する
        /// </summary>
        /// <param name="isEnemy"></param>
        /// <returns></returns>
        public static int GetUnitLayer(bool isEnemy)
        {
            return isEnemy ? LayerMask.NameToLayer("Enemy") : LayerMask.NameToLayer("Player");
        }

        public static float GetDirection(int layer)
        {
            return layer == LayerMask.NameToLayer("Player") ? 1.0f : -1.0f;
        }

        public static Vector3 GetRotation(int layer)
        {
            return layer == LayerMask.NameToLayer("Player") ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
        }
    }
}
