using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
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
        [FoldoutGroup("Attack Settings")] 
        
        [SerializeField] private Transform _point;

        protected Transform Point => _point == null ? transform : _point;

        [FormerlySerializedAs("Element")] 
        [FoldoutGroup("Attack Settings")] 
        [SerializeField] protected Element AttackElement;
        public Element Element => AttackElement;

        public bool IgnoreTargetInvulnerability = false;

        #endregion
        
        #region Damage Settings
        [FoldoutGroup("Damage Settings")]
        [SerializeField] protected float DamageAmount;
        
        [FoldoutGroup("Damage Settings")]
        [SerializeField] protected LayerMask Layers;
        #endregion

        public UnityAction OnHit;
        
        /// <summary>
        /// Damageable owning this Attack
        /// </summary>
        [FoldoutGroup("Damage Settings")]
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