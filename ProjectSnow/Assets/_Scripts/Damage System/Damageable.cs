using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using  System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;

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
        [FoldoutGroup("Health")]
        [SerializeField] protected float _startHealth = 1;
        [ReadOnly, FoldoutGroup("Health")]
        [SerializeField, ProgressBar("_startHealth", "_currentHealth", ColorGetter = "GetColorBar")] protected float _currentHealth = 1;

        private Color GetColorBar
        {
            get
            {
                return this._currentHealth >  _startHealth * .7f ? Color.green :
                    this._currentHealth > _startHealth * .3f ? Color.yellow : Color.red;
            }
        }
        
        [FoldoutGroup("Shield")]
        public Shield Shield = new Shield();
        [FoldoutGroup("Invulnerability")]
        [SerializeField] private bool _invulnerable = false;

        public bool IsDead
        {
            get;
            protected set;
        }
        #endregion

        #region Public Fields

        
        public Element Element => _element;

        public float StartHealth => _startHealth;

        public float CurrentHealth => _currentHealth;

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

        protected virtual void Start()
        {
            _currentHealth = _startHealth;
        }

        private void OnValidate()
        {
            if (_currentHealth > _startHealth)
                _currentHealth = _startHealth;
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

            float endDamage = EndDamage(incomingDamage);

            _currentHealth -= endDamage;

            incomingDamage.Damage = endDamage;

            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke(incomingDamage);
                IsDead = true;
                _currentHealth = 0;
            }
            else
                OnTakeDamage?.Invoke(incomingDamage);
        }

        protected float EndDamage(DamageInfo incomingDamage)
        {
            return DamageCalculations.CalculateDamageBasedInElements
            (
                incomingDamage.Damage, 
                _element, 
                incomingDamage.Element
            );
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

        public virtual void SetInvulnerable(bool value) => Invulnerable = value;

        #endregion

        #endregion
    }
}