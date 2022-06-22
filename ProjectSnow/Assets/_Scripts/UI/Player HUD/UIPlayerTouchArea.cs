using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Managers;
using Game.DamageSystem;
using Game.Enemy;

namespace Game.UI
{
    public class UIPlayerTouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private bool _canAttack = true;

        private void Start()
        {
            EnemyQueueManager.Instance.OnElementDecided += DisableAttack;
            EnemyQueueManager.Instance.OnAttack += EnableAttack;
            EnemyQueueManager.Instance.OnChangeEnemy += EnableAttack;
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnElementDecided -= DisableAttack;
            EnemyQueueManager.Instance.OnAttack -= EnableAttack;
            EnemyQueueManager.Instance.OnChangeEnemy -= EnableAttack;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_canAttack)
            {
                LevelManager.Instance.GetPlayer.GetPlayerAttack.TriggerAbility();
                return;
            }

            LevelManager.Instance.GetPlayer.GetPlayerShieldUsage.SetShieldState(true);
        }

        private void EnableAttack()
        {
            _canAttack = true;
        }
        
        private void EnableAttack(EnemyHealth health)
        {
            _canAttack = true;
        }

        private void DisableAttack(Element element)
        {
            _canAttack = false;
            LevelManager.Instance.GetPlayer.GetPlayerShieldUsage.SetShieldState(false);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            LevelManager.Instance.GetPlayer.GetPlayerShieldUsage.SetShieldState(false);
        }
    }
}