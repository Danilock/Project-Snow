using System;
using System.Collections;
using System.Collections.Generic;
using Game.Abilities;
using Game.DamageSystem.Attacks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(Attack))]
    public class PlayerAttack : BaseAbility
    {
        [SerializeField] private Attack _attack;
        
        private void Start()
        {
            PlayerInput.Instance.Actions.Player.Attack.canceled += AttackOncanceled;

            if (_attack == null)
                _attack = GetComponent<Attack>();
        }

        private void Update()
        {
            if (PlayerInput.Instance.AttackKeyPressed)
            {
                TriggerAbility();
            }
        }

        public override void TriggerAbility()
        {
            if(!CanUse)
                return;

            _attack.DoAttack();
            
            HandleCooldownCoroutine = HandleCooldown_CO();
                
            StartCoroutine(HandleCooldownCoroutine);
            
            OnAbilityUse?.Invoke();
        }

        private void AttackOncanceled(InputAction.CallbackContext obj)
        {
            StopCoroutine(HandleCooldownCoroutine);

            CanUse = true;
        }
    }
}