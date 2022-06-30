using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.DamageSystem;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Game.PostProcessing
{
    public class PostProcessingController : MonoBehaviour
    {
        [SerializeField] private PostProcessVolume _volume;
        [SerializeField] private Vignette _vignette;

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
            _vignette = _volume.profile.GetSetting<Vignette>();

            DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, value, _time);
        }
    }
}