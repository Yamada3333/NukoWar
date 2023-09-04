using System;
using System.Collections.Generic;
using UnityEngine;

namespace NukoWar.Scriptable
{
    [CreateAssetMenu(fileName = "Stage", menuName = "Scriptable/Stage")]
    [Serializable]
    public class StageScriptable : ScriptableObject
    {
        public string stageName;
        public List<Fave> faves;
        
        //favesが終わった場合の無制限に出る敵を設定
        public Fave unlimitedFave;
    }

    [Serializable]
    public struct Fave
    {
        public List<UnitScriptable> units;
        public int spawnTime;
    }
}
