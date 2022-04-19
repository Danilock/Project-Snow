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
        #region Killing Streak Phases
        [SerializeField] private List<KillingStreakPhase> _killingStreakPhases;
        [SerializeField, ReadOnly] private KillingStreakPhase _currentPhase;
        private int _index;
        #endregion

        #region Hit Streak
        [FoldoutGroup("Killing Streak")] 
        [SerializeField]
        private float _streakDuration = 2.5f;

        [SerializeField] private float _amountToIncrease = 1.3f;

        private IEnumerator _hitStreak;
        
        
        [FormerlySerializedAs("_hitStreakHasEnded")] [DisableInPlayMode, SerializeField, FoldoutGroup("Killing Streak")] private bool _hitStreakIsOff = false;

        [FoldoutGroup("Killing Streak"), SerializeField, DisableInPlayMode]
        private float _currentHitCount = 0;
        
        public static UnityAction StartHitStreak;
        public static UnityAction EndHitStreak;
        public static UnityAction<KillingStreakData> OnHit;
        public static UnityAction<KillingStreakPhase> OnChangePhase;
        #endregion

        #region Dependencies

        [SerializeField] private Attack _attack;

        #endregion

        private void Start()
        {
            if (_attack == null)
                _attack = GetComponent<Attack>();

            _attack.OnHit.AddListener(InitializeHitStreak);

            PickStreakPhaseByIndex(0);
        }

        /// <summary>
        /// Player will have a kill streak giving them a bonus attack power.
        /// </summary>
        private void InitializeHitStreak()
        {
            if (!_hitStreakIsOff)
            {
                StopCoroutine(_hitStreak);

                _hitStreak = KillingStreak_CO();

                StartCoroutine(_hitStreak);
            }

            _hitStreak = KillingStreak_CO();
            StartCoroutine(_hitStreak);
        }

        private IEnumerator KillingStreak_CO()
        {
            if(_hitStreakIsOff)
                StartHitStreak?.Invoke();

            _hitStreakIsOff = false;
            _currentHitCount *= _amountToIncrease;

            CheckIfCurrentStreakPhaseHasOvercome();

            OnHit?.Invoke(new KillingStreakData(_currentPhase, _currentHitCount));
            
            //Wait for seconds
            yield return new WaitForSeconds(_streakDuration);
            
            EndHitStreak?.Invoke();
            _hitStreakIsOff = true;
            _currentHitCount = 1;
            ResetAllPhases();
        }

        private void CheckIfCurrentStreakPhaseHasOvercome()
        {
            if (_index == _killingStreakPhases.Count - 1)
                return;

            if(_currentHitCount > _killingStreakPhases[_index + 1].AmounToOvercome)
            {
                _currentPhase.HasBeenOvercome = true;

                _index = (_index + 1) % _killingStreakPhases.Count;

                PickStreakPhaseByIndex(_index);
            }
        }

        private void PickStreakPhaseByIndex(int index)
        {
            _currentPhase = _killingStreakPhases[index];

            OnChangePhase?.Invoke(_currentPhase);
        }

        private void ResetAllPhases()
        {
            foreach(KillingStreakPhase currentPhase in _killingStreakPhases)
            {
                currentPhase.HasBeenOvercome = false;
            }

            _currentPhase = _killingStreakPhases[0];
        }
    }

    [System.Serializable]
    public class KillingStreakPhase
    {
        public string StrakPhaseName;
        public int AmounToOvercome;
        public bool HasBeenOvercome;
        public Color Color;
    }

    public class KillingStreakData
    {
        public KillingStreakPhase Phase;
        public float CurrentHitCount;

        public KillingStreakData(KillingStreakPhase phase, float currentHitCount)
        {
            Phase = phase;
            CurrentHitCount = currentHitCount;
        }
    }
}