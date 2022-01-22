using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyElementChange : MonoBehaviour
    {
        #region Private Fields
        [SerializeField] private List<EnemyHealthBar> _healthBars;
        [SerializeField, ReadOnly] private EnemyHealthBar _currentHealthBar;
        private int _currentBarIndex = 0;
        private Damageable _damageable;
        #endregion

        #region Public Fields

        public EnemyHealthBar GetCurrentHealthBar => _currentHealthBar;

        #endregion

        #region Properties

        /// <summary>
        /// If the current health of the enemy is lower than the Value specified in the current bar then we return true.
        /// </summary>
        private bool CurrentBarIsLowerThanHealthValue
        {
            get
            {
                if (_damageable.CurrentHealth < (float)_damageable.StartHealth * ((float)_currentHealthBar.Value / 100))
                {
                    return true;
                }

                return false;
            }
        }
        #endregion

        private void Awake()
        {
            _damageable = GetComponent<Damageable>();
        }

        private void Start()
        {
            SetBar(0);
        }

        private void OnEnable()
        {
            _damageable.OnTakeDamage += OnTakeDamageListener;
        }

        private void OnDisable()
        {
            _damageable.OnTakeDamage -= OnTakeDamageListener;
        }

        private void OnTakeDamageListener(DamageInfo info)
        {
            if (CurrentBarIsLowerThanHealthValue)
            {
                SetBar((_currentBarIndex + 1) % _healthBars.Count);
            }
        }

        private void SetBar(int index)
        {
            _currentHealthBar = _healthBars[index];
            
            _damageable.ChangeElement(_currentHealthBar.Element);
        }
    }
}