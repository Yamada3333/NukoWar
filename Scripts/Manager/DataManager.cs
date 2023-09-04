using NukoWar.Scriptable;
using UnityEngine;

namespace NukoWar.Scripts.Manager
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        public DataScriptable data;

        private void Awake()
        {
            Instance = this;
        }
    }
}
