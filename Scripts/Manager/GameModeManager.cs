using NukoWar.Scripts.Enum;
using NukoWar.Scripts.UnitHomeCamp;
using NukoWar.Scripts.View;
using UnityEngine;
using UniRx;

namespace NukoWar.Scripts.Manager
{
    public class GameModeManager : MonoBehaviour
    {
        public static GameModeManager Instance { get; private set; }
        
        public GameMode CurrentGameMode { get; private set; }

        public PlayerUnitHomeCamp playerUnitHomeCamp;
        public EnemyUnitHomeCamp enemyUnitHomeCamp;
        public TitleButton titleButton;
        
        private void Awake()
        {
            Instance = this;
            SetGameMode(GameMode.Play);
        }

        private void Start()
        {
            playerUnitHomeCamp.CurrentHealth.Subscribe(health => WinnerCheck(health, "You Lose..."));
            enemyUnitHomeCamp.CurrentHealth.Subscribe(health => WinnerCheck(health, "You Win!"));
        }

        private void WinnerCheck(int health, string message)
        {
            if (health > 0) return;
            SetGameMode(GameMode.Pose);
            titleButton.SetText(message);
        }

        private void SetGameMode(GameMode gameMode)
        {
            CurrentGameMode = gameMode;
        }
    }
}
