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

        [SerializeField] private int _attackIndex;

        [SerializeField] private Transform _shieldTransform;

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

            _shieldTransform.localPosition = new Vector3(_attackIndex != 0f ? -0.12f : 0.52f, _shieldTransform.transform.localPosition.y, _shieldTransform.localPosition.z);

            _attackIndex = (_attackIndex + 1) % 2;
        }
    }
}