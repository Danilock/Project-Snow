using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Managers;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Game.UI
{
    public class UIEnemyCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _counterText;
        [SerializeField] private Transform _monsterIcon;
        [SerializeField, FoldoutGroup("Tween Settings")] private Vector2 _positionPunch;
        [SerializeField, FoldoutGroup("Tween Settings")] private float _duration;

        private void Start()
        {
            EnemyQueueManager.Instance.OnChangeEnemy += UpdateEnemyCount;

            UpdateText();
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnChangeEnemy += UpdateEnemyCount;
        }

        private void UpdateEnemyCount(Game.Enemy.EnemyHealth health)
        {
            UpdateText();
            PunchIcon();
        }

        private void UpdateText()
        {
            _counterText.text = $"{EnemyQueueManager.Instance.GetCurrentIndex + 1} - {EnemyQueueManager.Instance.GetAmountOfEnemies}";
        }

        [Button("DoPunch")]
        private void PunchIcon()
        {
            _monsterIcon.DOPunchPosition(_positionPunch, _duration);
        }
    }
}