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
        [SerializeField] private PlayerHealth _damageable;
        [SerializeField, Range(0, 100)] private float _shieldDamageOnBlock = 20f;

        [SerializeField] private float _shieldDuration = 1f;


        #region Shield
        [SerializeField] private GameObject _shieldGameObject;

        Material _shieldMaterial;
        #endregion

        public float ShieldDamageOnBlock => _shieldDamageOnBlock;

        private void Start()
        {
            if (_damageable == null)
                _damageable = GetComponent<PlayerHealth>();
            
            PlayerInput.Instance.Actions.Player.Shield.performed += ShieldOnperformed;

            _damageable.OnShieldBlock.AddListener(() => EnergySource.UseEnergy(RequiredEnergy));

            _shieldMaterial = _shieldGameObject.GetComponent<Renderer>().material;

            _shieldMaterial.SetFloat("_Alpha", 0f);
        }

        private void ShieldOnperformed(InputAction.CallbackContext obj)
        {
            this.TriggerAbility();
        }

        public override void TriggerAbility()
        {
            if(!CanUseAbility)
                return;

            StartCoroutine(HandleShieldDuration_CO());
        }

        private IEnumerator HandleShieldDuration_CO()
        {
            SetShieldState(true);
            yield return new WaitForSeconds(_shieldDuration);
            SetShieldState(false);
        }

        public void SetShieldState(bool state)
        {
            _damageable.Shield.IsActive = state;

            SetShieldShader(state);
        }

        #region Art
        public void SetShieldShader(bool state)
        {
            if (state)
            {
                _shieldMaterial?.SetColor("_OutlineColor", _damageable.Shield.Element.ShieldColor);
                _shieldMaterial?.SetColor("_HologramStripeColor", _damageable.Shield.Element.ShieldColor);
                _shieldMaterial?.SetColor("_Color", _damageable.Shield.Element.ShieldColor);
            }

            _shieldMaterial?.DOFloat(state ? 1f : 0f, "_Alpha", .4f);
        }
        #endregion
    }
}