using System;
using UnityEngine;

namespace NukoWar.Scriptable
{
    [CreateAssetMenu(fileName = "Unit", menuName = "Scriptable/Unit")]
    [Serializable]
    public class UnitScriptable : ScriptableObject
    {
        public string unitName;
        public Sprite unitSprite;
        public Sprite cardSprite;
        public int cost = 100;
        public int hp = 100;
        public int attack = 10;
        public float attackInterval = 1.0f;
        public float moveSpeed = 1.0f;
        public float attackRange = 1.0f;
    }
}
