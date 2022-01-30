using System;
using System.Collections;
using System.Collections.Generic;
using Game.AbilitySystem;
using Game.DamageSystem;
using Game.DamageSystem.Attacks;
using Game.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyAttack : BaseAbility
{
    #region Cooldown and Attack
    
    [FoldoutGroup("Minimum and Maximun cooldown")]
    [SerializeField] private float _min, _max;

    [SerializeField] private Attack _attack;
    [FormerlySerializedAs("_seconds")] [SerializeField, ReadOnly, HideInEditorMode] private float _secondsBeforeChargeAttack;

    #endregion

    #region Elements and charge variables

    [FoldoutGroup("Possible Attack Elements")] 
    [SerializeField] private List<Element> _possibleElements;

    [SerializeField] private float _secondsChargeAttack = 1.5f;

    private IEnumerator _chargeAttackCoroutine;

    private Element PickRandomElement
    {
        get
        {
            return _possibleElements[Random.Range(0, _possibleElements.Count - 1)];
        }
    }

    #endregion
    #region Animator

    [FoldoutGroup("Animator")] 
    [SerializeField]
    private Animator _animator;

    private static readonly int HashChargeAttack = Animator.StringToHash("ChargeAttack");
    private static readonly int HashAttack = Animator.StringToHash("Attack");

    #endregion

    #region Health

    private EnemyHealth _health;

    #endregion
    
    #region Unity Methods
    
    protected override void Awake()
    {
        HandleCooldownCoroutine = HandleCooldown_CO();
        _chargeAttackCoroutine = ChargeAttack_CO();

        _animator = GetComponent<Animator>();
        _health = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        _attack = GetComponent<Attack>();

        StartCoroutine(HandleCooldownCoroutine);
    }

    private void OnValidate()
    {
        if (_min > _max)
            _max = _min;
    }
    #endregion

    protected override IEnumerator HandleCooldown_CO()
    {
        CanUse = false;

        _secondsBeforeChargeAttack = Random.Range(_min, _max);
        
        _attack.ChangeElement(PickRandomElement);
        
        yield return new WaitForSeconds(_secondsBeforeChargeAttack);

        _chargeAttackCoroutine = ChargeAttack_CO();
        
        StartCoroutine(_chargeAttackCoroutine);
    }

    public void Attack()
    {
        HandleCooldownCoroutine = HandleCooldown_CO();
        
        _attack.DoAttack();

        StartCoroutine(HandleCooldownCoroutine);
    }

    private IEnumerator ChargeAttack_CO()
    {
        _animator.SetTrigger(HashChargeAttack);
        _health.SetInvulnerable(true);
        
        yield return new WaitForSeconds(_secondsChargeAttack);
        
        _animator.SetTrigger(HashAttack);
        _health.SetInvulnerable(false);
    }
}
