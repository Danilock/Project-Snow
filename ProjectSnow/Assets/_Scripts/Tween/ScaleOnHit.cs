using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.DamageSystem;
using UnityEngine;

namespace Game.Tween
{
    [AddComponentMenu("Tween/ScaleOnHit")]
    [RequireComponent(typeof(Damageable))]
    public class ScaleOnHit : MonoBehaviour
    {
        [Header("Tween Settings")] 
        [SerializeField] private Vector2 _punchScaleSize;

        [SerializeField] private float _duration = 1;
        [SerializeField] private int _elasticity;
        [SerializeField] private int _vibration;

        [Header("Target")] 
        [SerializeField, Tooltip("Visual object to apply the tween scale")] private GameObject _target;

        [Header("Flags")] 
        [SerializeField] private bool _canPunch = true;
        
        private Damageable _dmg;
        // Start is called before the first frame update
        void Awake()
        {
            _dmg = GetComponent<Damageable>();
        }

        private void OnEnable()
        {
            _dmg.OnTakeDamage += DoScale;
        }

        private void OnDisable()
        {
            _dmg.OnTakeDamage -= DoScale;
        }

        private void DoScale(DamageInfo arg0)
        {
            if(!_canPunch)
                return;

            _canPunch = false;
            
            _target.transform.DOPunchScale(_punchScaleSize, _duration, _vibration, _elasticity).OnComplete(() => _canPunch = true);
        }
    }
}