using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.DamageSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Tween
{
    [AddComponentMenu("Tween/DoScale")]
    [RequireComponent(typeof(Damageable))]
    public class DoScale : MonoBehaviour
    {
        [FoldoutGroup("Tween Settings")] 
        [SerializeField] private Vector2 _punchScaleSize;

        [FoldoutGroup("Tween Settings")] 
        [SerializeField] private float _duration = 1;
        
        [FoldoutGroup("Tween Settings")] 
        [SerializeField] private int _elasticity;
        
        [FoldoutGroup("Tween Settings")] 
        [SerializeField] private int _vibration;

        [Header("Target")] 
        [SerializeField, Tooltip("Visual object to apply the tween scale")] private GameObject _target;

        [Header("Flags")] 
        [SerializeField] private bool _canPunch = true;

        public void Scale()
        {
            if(!_canPunch)
                return;

            _canPunch = false;
            
            _target.transform.DOPunchScale(_punchScaleSize, _duration, _vibration, _elasticity).OnComplete(() => _canPunch = true);
        }

        public void SetScaleProperties(Vector3 punchScaleSize, float duration, int vibration, int elasticity)
        {
            _punchScaleSize = punchScaleSize;
            _duration = duration;
            _vibration = vibration;
            _elasticity = elasticity;
        }
    }
}