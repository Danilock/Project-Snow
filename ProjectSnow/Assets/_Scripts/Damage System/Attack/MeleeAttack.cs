using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.DamageSystem.Attacks
{
    public class MeleeAttack : Attack
    {
        [FoldoutGroup("Melee Attack Settings")] 
        [SerializeField] private Vector2 DamageAreaSize;
        
        #region Attack Point Variables
        private enum FocusOn
        {
            Offset,
            Target
        }

        [FoldoutGroup("Melee Attack Settings")] 
        [SerializeField] private FocusOn _focusOn;

        private Vector2 _determinePoint
        {
            get
            {
                if (_focusOn == FocusOn.Offset)
                    return (Vector2)transform.position + _offset;
                else
                {
                    return Point.position;
                }
            }
        }

        [FoldoutGroup("Melee Attack Settings")] 
        [SerializeField] private Vector2 _offset;
        #endregion
        
        [FoldoutGroup("Gizmos")] 
        [SerializeField] private Color _gizmoColor;
        
        /// <summary>
        /// Does an attack in a square area.
        /// </summary>
        public override void DoAttack()
        {
            //Generates a collider looking for objects
            Collider2D[] damageableHit = Physics2D.OverlapBoxAll(_determinePoint, DamageAreaSize, 0f, Layers);
            
            //Going through all damageables detected
            foreach (Collider2D damageable in damageableHit)
            {
                Damageable dmg = damageable.GetComponent<Damageable>();
                
                if(dmg == null)
                    return;
                
                dmg.DoDamage(new DamageInfo(Owner, DamageAmount, IgnoreTargetInvulnerability, AttackElement));
                
                if(!dmg.Invulnerable && dmg.Element == Element && !dmg.IsDead)
                    OnHit?.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            _gizmoColor.a = 1f;
            
            Gizmos.color = _gizmoColor;
            
            Gizmos.DrawWireCube(_determinePoint, DamageAreaSize);
        }
    }
}