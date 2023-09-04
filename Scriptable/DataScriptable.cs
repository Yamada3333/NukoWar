using System;
using System.Collections.Generic;
using UnityEngine;

namespace NukoWar.Scriptable
{
    /// <summary>
    /// シーン共通のデータを管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "Data", menuName = "Scriptable/Data")]
    [Serializable]
    public class DataScriptable : ScriptableObject
    {
        //全てのユニットを設定
        public UnitScriptable[] allUnits;
        
        //召喚できるユニットを設定
        //要素数を変更するとエラーになる
        public UnitScriptable[] selectedUnits = new UnitScriptable[5];

        //プレイヤーのお金関係
        public int maxMoney = 1000;
        public int currentMoney = 100;
        public int recoveryMoney = 100;
        public float moneyRecoveryTime = 4.0f;
        //円やドルなどの通貨単位
        public string currencyUnit = "エン";
        
        //ステージの情報
        public StageScriptable currentStage;
        public List<StageScriptable> allStage = new List<StageScriptable>();
    }
}
