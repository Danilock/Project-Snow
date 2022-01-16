using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using  System.Linq;

namespace DamageSystem
{
    /// <summary>
    /// Representation of a damageable entity in game.
    /// </summary>
    public class Damageable : MonoBehaviour
    {
        #region Protected/Private Fields
        [SerializeField] protected Element _element;
        
        //Health
        [SerializeField] protected int _startHealth = 1;
        [SerializeField] protected int _currentHealth = 1;

        [SerializeField] protected Shield _shield = new Shield();
        
        [SerializeField] protected bool _invulnerable = false;
        #endregion

        #region Public Fields

        
        public Element Element => _element;

        public int StartHealth => _startHealth;

        public int CurrentHealth => _currentHealth;

        public bool Invulnerable => _invulnerable;

        public Shield Shield => _shield;

        #endregion
        
        #region Events

        public UnityAction<DamageInfo> OnTakeDamage, OnDead;

        #endregion

        #region Methods

        /// <summary>
        /// Do damage to this entity by receiving a damage info struct.
        /// </summary>
        /// <param name="incomingDamage"></param>
        public virtual void DoDamage(DamageInfo incomingDamage)
        {
            if (_invulnerable)
                return;

            if (_shield.ShieldAmount > 0)
            {
                _shield.DamageShield(incomingDamage);
                return;
            }

            _currentHealth -= DamageCalculations.CalculateDamageBasedInElements
                (
                    incomingDamage.Damage, 
                    _element, 
                    incomingDamage.Transmitter.Element
                );

            if (_currentHealth <= 0)
                OnDead?.Invoke(incomingDamage);
            else
                OnTakeDamage?.Invoke(incomingDamage);
        }

        /// <summary>
        /// Sets the health immediately to the specified value.
        /// </summary>
        /// <param name="value"></param>
        public void SetHealth(int value)
        {
            if (value > _startHealth)
                _startHealth = value;

            _currentHealth = value;
        }

        public void SetShield(int value) => _shield.SetShieldAmount(value);

        #endregion
    }

    public class DamageCalculations
    {
        /// <summary>
        /// Calculate damage based in the elements of the two objects. Adding or removing damage to the transmitter.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static int CalculateDamageBasedInElements(int damageAamount, Element receiver, Element transmitter)
        {
            if (receiver == null || transmitter == null)
                return damageAamount;
            
            int damageResult = damageAamount;

            if (transmitter.StrongAgainst.Contains(receiver))
                damageResult += (int)(damageResult * .25);
            else if (transmitter.WeakAgainst.Contains(receiver))
                damageResult -= (int) (damageResult * .50);

            return damageResult;
        }
    }
}