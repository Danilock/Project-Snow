using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using UnityEngine;

namespace Game.Player
{
    public class PlayerHealth : Damageable
    {
        public override void DoDamage(DamageInfo incomingDamage)
        {
            if(Invulnerable)
                return;
            
            if (Shield.ShieldAmount > 0 && Shield.IsActive)
            {
                Shield.DamageShield(incomingDamage);
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
