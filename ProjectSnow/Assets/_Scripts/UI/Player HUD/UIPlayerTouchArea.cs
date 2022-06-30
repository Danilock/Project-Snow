using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Managers;
using Game.DamageSystem;
using Game.Enemy;
using EventDriven;

namespace Game.UI
{
    public class UIPlayerTouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventListener<DisallowPlayerToAttack>
    {
        [SerializeField] private bool _canAttack = true;

        private void Start()
        {
            EnemyQueueManager.Instance.OnAttack += EnableAttack;
            EnemyQueueManager.Instance.OnChangeEnemy += EnableAttack;
            this.StartListening<DisallowPlayerToAttack>();
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnAttack -= EnableAttack;
            EnemyQueueManager.Instance.OnChangeEnemy -= EnableAttack;
            this.StopListening<DisallowPlayerToAttack>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_canAttack)
            {
                LevelManager.Instance.GetPlayer.GetPlayerAttack.TriggerAbility();
                return;
            }

            LevelManager.Instance.GetPlayer.GetPlayerShieldUsage.TriggerAbility();
        }

        private void EnableAttack()
        {
            StartCoroutine(SetAttack(true));
        }
        
        private void EnableAttack(EnemyHealth health)
        {
            StartCoroutine(SetAttack(true));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            LevelManager.Instance.GetPlayer.GetPlayerShieldUsage.SetShieldState(false);
        }

        private IEnumerator SetAttack(bool value)
        {
            yield return new WaitForSeconds(.3f);
            _canAttack = value;
        }

        public void OnTriggerEvent(DisallowPlayerToAttack data)
        {
            _canAttack = false;
        }
    }
}