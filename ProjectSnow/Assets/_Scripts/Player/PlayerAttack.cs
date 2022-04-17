using System;
using System.Collections;
using System.Collections.Generic;
using Game.AbilitySystem;
using Game.DamageSystem.Attacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(Attack))]
    public class PlayerAttack : BaseAbility
    {
        [SerializeField] private Attack _attack;

        [FoldoutGroup("On Hit"), SerializeField] private float _energyToRecover = 3f;

        private PlayerHealth _health;

        private EnergySource _energy;

        private void Start()
        {
            PlayerInput.Instance.Actions.Player.Attack.canceled += AttackOncanceled;

            _health = GetComponent<PlayerHealth>();
            _energy = GetComponent<EnergySource>();

            if (_attack == null)
                _attack = GetComponent<Attack>();

            _attack.OnHit.AddListener(RecoverEnergyOnHit);
        }

        /// <summary>
        /// Adding energy to player's energy source everytime we hit something.
        /// </summary>
        private void RecoverEnergyOnHit() => _energy.AddEnergy(_energyToRecover);

        private void Update()
        {
            if (PlayerInput.Instance.AttackKeyPressed)
                TriggerAbility();
        }

        public override void TriggerAbility()
        {
            if (!CanUse || _health.Shield.IsActive)
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