using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.DamageSystem.Attacks
{
    /// <summary>
    /// Base class for attacks.
    /// </summary>
    public abstract class Attack : MonoBehaviour, IElemental
    {
        #region Attack Settings
        [FormerlySerializedAs("Point")]
        [Header("Attack Settings")] 
        
        [SerializeField] private Transform _point;

        protected Transform Point => _point == null ? transform : _point;

        [FormerlySerializedAs("Element")] 
        [SerializeField] protected Element AttackElement;
        public Element Element => AttackElement;

        #endregion
        
        #region Damage Settings
        [Header("Damage Settings")]
        [SerializeField] protected int DamageAmount;
        
        [SerializeField] protected LayerMask Layers;
        #endregion
        
        /// <summary>
        /// Damageable owning this Attack
        /// </summary>
        public Damageable Owner;
        public abstract void DoAttack();

        protected virtual void Awake()
        {
            if (Owner == null)
                Owner = GetComponent<Damageable>();
        }

        public void ChangeElement(Element newElement)
        {
            AttackElement = newElement;
        }
    }
}