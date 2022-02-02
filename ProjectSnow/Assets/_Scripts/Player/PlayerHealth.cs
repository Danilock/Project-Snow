using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using UnityEngine;

namespace Game.Player
{
    public class PlayerHealth : Damageable
    {
        private PlayerShieldUsage _shieldUsage;

        protected override void Start()
        {
            base.Start();

            _shieldUsage = GetComponent<PlayerShieldUsage>();
        }

        public override void DoDamage(DamageInfo incomingDamage)
        {
            if(Invulnerable)
                return;
            
            //If the player receives damage when using a counter shield, then we damage the transmitter.
            if ((Shield.Element.IsCounterOf(incomingDamage.Transmitter.Element)) && Shield.IsActive)
            {
                incomingDamage.Transmitter.DoDamage(
                    new DamageInfo(
                    this, _shieldUsage.ShieldDamageOnBlock, true)
                    );
                return;
            }

            _currentHealth -= DamageCalculations.CalculateDamageBasedInElements
            (
                incomingDamage.Damage, 
                _element, 
                incomingDamage.Transmitter.Element
            );

            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke(incomingDamage);
                IsDead = true;
                _currentHealth = 0;
            }
            else
                OnTakeDamage?.Invoke(incomingDamage);
        }
    }
}
