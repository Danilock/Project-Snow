using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.DamageSystem;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Game.PostProcessing
{
    public class PostProcessingController : MonoBehaviour
    {
        [SerializeField]private Volume _volume;
        private Vignette _vignette;

        [Header("Values")] 
        [SerializeField] private float _valueOnNormalScreen = 0.459f;
        [SerializeField] private float _valueOnAttack = 0.34f;

        [Header("Time")] 
        [SerializeField] private float _time;

        private void Start()
        {
            EnemyQueueManager.Instance.OnElementDecided += ShowAttackVignette;
            EnemyQueueManager.Instance.OnAttack += ShowNormalVignette;
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnElementDecided -= ShowAttackVignette;
            EnemyQueueManager.Instance.OnAttack -= ShowNormalVignette;
        }

        [Button("Show Attack Vignette")]
        void ShowAttackVignette(Element element) => SetVignetteIntensityValue(_valueOnAttack);

        [Button("Show Normal Vignette")]
        void ShowNormalVignette() => SetVignetteIntensityValue(_valueOnNormalScreen);

        private void SetVignetteIntensityValue(float value)
        {
            _volume.profile.TryGet(out _vignette);

            DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, value, _time);
        }
    }
}