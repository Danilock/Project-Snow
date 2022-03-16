using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using Game.Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Player
{
    public class PlayerHealth : Damageable
    {
        [SerializeField] private UnityEvent _onShieldBlock;
        
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
                if ((Shield.Element.IsCounterOf(incomingDamage.Element)))
                {
                    Shield.IsActive = false;

                    EnemyHealthBar bar = incomingDamage.Transmitter.GetComponent<EnemyHealth>().CurrentHealthBar;
                    
                    float damageAmount = bar.StartValue *
                                         (_shieldUsage.ShieldDamageOnBlock / 100);
                    
                    incomingDamage.Transmitter.DoDamage(
                        new DamageInfo(
                            this, damageAmount, true, Shield.Element)
                    );
                    
                    _onShieldBlock?.Invoke();
                    return;
                }

                if (Shield.Element == incomingDamage.Element)
                {
                    Shield.IsActive = false;
                    return;
                }

                if (Shield.Element.IsWeakerThan(incomingDamage.Element))
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
