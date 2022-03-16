using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Game.DamageSystem;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace Game.Enemy
{
    public class EnemyHealth : Damageable
    {
        [SerializeField] private List<EnemyHealthBar> _healthBars;

        public List<EnemyHealthBar> HealthBars => _healthBars;

        [ReadOnly] public EnemyHealthBar CurrentHealthBar;

        public UnityAction<EnemyHealthBar> OnChangeBar;

        [SerializeField] private int _currentIndex = 0;

        [SerializeField] private UnityEvent _onHit;

        private bool IsLastHealthBar
        {
            get
            {
                return _currentIndex == _healthBars.Count - 1;
            }
        }
        public override void DoDamage(DamageInfo incomingDamage)
        {
            if ((Invulnerable && !incomingDamage.IgnoreInvulnerability) || IsDead)
                return;

            Debug.Log($"Damage: {incomingDamage.Damage} ---- Element: {incomingDamage.Element}");
            
            float endDamage = incomingDamage.IgnoreElement ? incomingDamage.Damage : DamageCalculations.CalculateDamageBasedInElements
            (
                incomingDamage.Damage, 
                _element, 
                incomingDamage.Element
            );

            incomingDamage.Damage = endDamage;
            
            Debug.LogError(endDamage);
            
            CurrentHealthBar.CurrentValue -= endDamage;

            if (CurrentHealthBar.CurrentValue <= 0)
            {
                if (CurrentHealthBar.IsLastHealthBar)
                {
                    IsDead = true;
                    OnDeath?.Invoke(incomingDamage);
                }
                else
                {
                    OnTakeDamage?.Invoke(incomingDamage);
                    _onHit.Invoke();
                    SelectNextBar();
                }
            }
            
            OnTakeDamage?.Invoke(incomingDamage);
            _onHit.Invoke();
        }

        public void InitializeBars()
        {
            foreach (EnemyHealthBar currentBar in _healthBars)
            {
                currentBar.CurrentValue = currentBar.StartValue;
            }

            _healthBars[_healthBars.Count - 1].IsLastHealthBar = true;
        }

        public void SelectNextBar()
        {
            CurrentHealthBar.OnDestroybar?.Invoke();
            
            _currentIndex = (_currentIndex + 1) % _healthBars.Count;
            
            SelectBar(_currentIndex);
        }

        public void SelectBar(int index)
        {
            CurrentHealthBar = _healthBars[index];
            
            ChangeElement(CurrentHealthBar.Element);
            
            OnChangeBar?.Invoke(CurrentHealthBar);
        }

        public void SetHealthBars(List<EnemyHealthBar> newBarsList) => _healthBars = newBarsList;
    }
}