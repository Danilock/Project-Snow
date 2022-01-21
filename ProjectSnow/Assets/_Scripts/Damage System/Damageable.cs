using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using  System.Linq;

namespace Game.DamageSystem
{
    /// <summary>
    /// Representation of a damageable entity in game.
    /// </summary>
    public class Damageable : MonoBehaviour, IElemental
    {
        #region Protected/Private Fields
        [SerializeField] protected Element _element;
        
        //Health
        [SerializeField] protected int _startHealth = 1;
        [SerializeField] protected int _currentHealth = 1;

        public Shield Shield = new Shield();
        
        [SerializeField] private bool _invulnerable = false;

        public bool IsDead
        {
            get;
            protected set;
        }
        #endregion

        #region Public Fields

        
        public Element Element => _element;

        public int StartHealth => _startHealth;

        public int CurrentHealth => _currentHealth;

        public bool Invulnerable
        {
            get => _invulnerable;
            protected set => _invulnerable = value;
        }

        #endregion
        
        #region Events

        public UnityAction<DamageInfo> OnTakeDamage, OnDeath;

        #endregion

        #region Methods

        protected void Start()
        {
            _currentHealth = _startHealth;
        }

        private void OnValidate()
        {
            if (_currentHealth > _startHealth)
                _startHealth = _currentHealth;
        }

        /// <summary>
        /// Do damage to this entity by receiving a damage info struct.
        /// </summary>
        /// <param name="incomingDamage"></param>
        public virtual void DoDamage(DamageInfo incomingDamage)
        {
            if (_invulnerable || IsDead)
                return;

            if (Shield.ShieldAmount > 0)
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

        /// <summary>
        /// Sets the health immediately to the specified value.
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetHealth(int value)
        {
            if (value > _startHealth)
                _startHealth = value;

            _currentHealth = value;
        }

        public virtual void ChangeElement(Element newElement)
        {
            _element = newElement;
        }

        #region Invulnerability
        public virtual void SetInvulnerableForSeconds(float seconds) => StartCoroutine(SetInvulnerableForSeconds_CO(seconds));

        protected virtual IEnumerator SetInvulnerableForSeconds_CO(float seconds)
        {
            Invulnerable = true;
            yield return new WaitForSeconds(seconds);
            Invulnerable = false;
        }
        #endregion

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

            //If the transmitter has a weakest element then we increase the damage by 0.25%
            if (transmitter.StrongAgainst.Contains(receiver))
                damageResult += (int) (damageResult * .25);
            
            //Otherwise, we decrease the damage by 50% 
            else if (transmitter.WeakAgainst.Contains(receiver))
                damageResult -= (int) (damageResult * .50);

            return damageResult;
        }
    }
}