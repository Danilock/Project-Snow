using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using Game.AbilitySystem;
using Game.DamageSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerShieldUsage : BaseAbility
    {
        [SerializeField] private Damageable _damageable;

        private void Start()
        {
            if (_damageable == null)
                _damageable = GetComponent<Damageable>();
            
            PlayerInput.Instance.Actions.Player.Shield.performed += ShieldOnperformed;
            PlayerInput.Instance.Actions.Player.Shield.canceled += ShieldOncanceled;
        }

        private void ShieldOnperformed(InputAction.CallbackContext obj)
        {
            this.TriggerAbility();
        }

        private void ShieldOncanceled(InputAction.CallbackContext obj)
        {
            _damageable.Shield.IsActive = false;
        }

        public override void TriggerAbility()
        {
            if(!CanUseAbility)
                return;

            EnergySource.UseEnergy(RequiredEnergy);
            _damageable.Shield.IsActive = true;
        }
    }
}