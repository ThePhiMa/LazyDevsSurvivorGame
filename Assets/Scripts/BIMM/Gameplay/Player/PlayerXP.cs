using System;
using UnityEngine;
using BIMM.Data;

namespace BIMM.Gameplay.Player
{
    public class PlayerXP : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;

        public static event Action<int> OnLevelUp;

        private float _currentXP;
        private int _currentLevel;

        public float CurrentXP => _currentXP;
        public int CurrentLevel => _currentLevel;
        public float XPForNextLevel => GetXPForNextLevel();

        private void Start()
        {
            _currentLevel = 1;
            _currentXP = 0f;
        }

        public void AddXP(float amount)
        {
            _currentXP += amount;

            if (_currentXP >= GetXPForNextLevel())
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _currentLevel++;
            _currentXP = 0f;

            Debug.Log($"Level up! Now level {_currentLevel}.");
            OnLevelUp?.Invoke(_currentLevel);
        }

        private float GetXPForNextLevel()
        {
            int index = _currentLevel - 1;

            if (index >= _data.XPThresholds.Length)
            {
                return float.MaxValue;
            }

            return _data.XPThresholds[index];
        }
    }
}
