using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using ObjectPooling;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game.UI
{
    public class DamageHitUIEventTrigger : MonoBehaviour
    {
        [FoldoutGroup("Pool")]
        [SerializeField] private string _hitStatsPool;
        [FoldoutGroup("Effect position")]
        [SerializeField] private Transform _effectPosition;
        
        private Damageable _damageable;

        private void Awake()
        {
            _damageable = GetComponent<Damageable>();
        }

        private void OnEnable()
        {
            _damageable.OnTakeDamage += ShowHitUIStat;
            _damageable.OnMiss += ShowMissText;
        }

        private void OnDisable()
        {
            _damageable.OnTakeDamage -= ShowHitUIStat;
            _damageable.OnMiss -= ShowMissText;
        }

        /// <summary>
        /// Instantiate a state everytime the damageable object is hit.
        /// The stat will show the amount of damage this damageable is receiving.
        /// </summary>
        /// <param name="info"></param>
        private void ShowHitUIStat(DamageInfo info)
        {
            GameObject obj = ObjectPooler.Instance.GetObjectFromPool(_hitStatsPool);
            DamageHitUI hitUi = obj.GetComponent<DamageHitUI>();

            obj.transform.position = _effectPosition.position;
            hitUi.Init(info);
        }

        private void ShowMissText()
        {
            GameObject obj = ObjectPooler.Instance.GetObjectFromPool(_hitStatsPool);
            DamageHitUI hitUi = obj.GetComponent<DamageHitUI>();

            obj.transform.position = transform.position;
            hitUi.ShowMissText();
        }
    }
}