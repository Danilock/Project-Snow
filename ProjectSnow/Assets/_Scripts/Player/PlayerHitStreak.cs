using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem.Attacks;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game.Player
{
    public class PlayerHitStreak : SceneSingleton<PlayerHitStreak>
    {
        #region Hit Streak
        [FoldoutGroup("Killing Streak")] 
        [SerializeField]
        private float _streakDuration = 2.5f;
        private IEnumerator _hitStreak;
        [FormerlySerializedAs("_hitStreakHasEnded")] [DisableInPlayMode, SerializeField, FoldoutGroup("Killing Streak")] private bool _hitStreakIsOff = false;

        [FoldoutGroup("Killing Streak")] [SerializeField]
        private int _incrementPerHit = 2;

        [FoldoutGroup("Killing Streak"), SerializeField, DisableInPlayMode]
        private int _currentHitCount = 0;
        
        public static UnityAction StartHitStreak;
        public static UnityAction EndHitStreak;
        public static UnityAction<int> OnHit;
        #endregion

        #region Dependencies

        [SerializeField] private Attack _attack;

        #endregion

        private void Start()
        {
            if (_attack == null)
                _attack = GetComponent<Attack>();

            _attack.OnHit.AddListener(InitializeHitStreak);
        }

        /// <summary>
        /// Player will have a kill streak giving them a bonus attack power.
        /// </summary>
        private void InitializeHitStreak()
        {
            _hitStreak = KillingStreak_CO();
            StartCoroutine(_hitStreak);
        }

        private IEnumerator KillingStreak_CO()
        {
            if(_hitStreakIsOff)
                StartHitStreak?.Invoke();

            _currentHitCount *= 2;
            OnHit?.Invoke(_currentHitCount);
            yield return new WaitForSeconds(_streakDuration);
            EndHitStreak?.Invoke();
            _hitStreakIsOff = true;
            _currentHitCount = 1;
        }
    }
}