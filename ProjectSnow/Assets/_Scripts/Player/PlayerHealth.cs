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


            if (Shield.IsActive)
            {
                //If the player receives damage when using a counter shield, then we damage the transmitter.
                if ((Shield.Element.IsCounterOf(incomingDamage.Transmitter.Element)))
                {
                    Shield.IsActive = false;
                    
                    incomingDamage.Transmitter.DoDamage(
                        new DamageInfo(
                            this, _shieldUsage.ShieldDamageOnBlock, true)
                    );
                    return;
                }

                if (Shield.Element == incomingDamage.Transmitter.Element)
                {
                    Shield.IsActive = false;
                    return;
                }

                if (Shield.Element.IsWeakerThan(incomingDamage.Transmitter.Element))
                {
                    Shield.IsActive = false;
                    KillPlayer(incomingDamage);
                    return;
                }
            }

            _currentHealth -= EndDamage(incomingDamage);

            if (_currentHealth <= 0)
            {
                KillPlayer(incomingDamage);
            }
            else
                OnTakeDamage?.Invoke(incomingDamage);
        }

        public void KillPlayer(DamageInfo incomingDamage)
        {
            OnDeath?.Invoke(incomingDamage);
            IsDead = true;
            _currentHealth = 0;
        }
    }
}
