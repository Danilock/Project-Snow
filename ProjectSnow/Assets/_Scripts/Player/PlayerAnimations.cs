using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        
        #region Animator

        [FoldoutGroup("Animator")]
        [SerializeField] private Animator _animator;

        #endregion
        
        #region Attack

        [FoldoutGroup("Attak Animation")] [SerializeField]
        private int _animationsCount = 2;

        [SerializeField] private int _attackIndex;

        private PlayerAttack _attack;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int AttackIndex = Animator.StringToHash("AttackIndex");

        #endregion

        // Start is called before the first frame update
        void Awake()
        {
            _attack = GetComponent<PlayerAttack>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _attack.OnAbilityUse += AnimateAttack;
        }

        private void OnDisable()
        {
            _attack.OnAbilityUse -= AnimateAttack;
        }

        private void AnimateAttack()
        {
            _animator.SetTrigger(Attack);
            _animator.SetInteger(AttackIndex, _attackIndex);

            _attackIndex = (_attackIndex + 1) % 2;
        }
    }
}