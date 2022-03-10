using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.DamageSystem;
using Game.Enemy;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// Shows in the UI the current enemy attack element
    /// </summary>
    public class UIEnemyAttackElement : MonoBehaviour
    {

        [SerializeField, FoldoutGroup("Settings")] private Image _currentElementImage;

        private Image _image;

        #region Tweening

        [SerializeField, FoldoutGroup("Tweening")]
        private Vector3 _scale;

        [SerializeField, FoldoutGroup("Tweening")]
        private float _duration = .3f;

        [SerializeField, FoldoutGroup("Tweening")]
        private int _vibration = 10;

        [SerializeField, FoldoutGroup("Tweening")]
        private float _elasticity = 1f;

        #endregion

        private void Start()
        {
            EnemyQueueManager.Instance.OnElementDecided += UpdateImage;
            EnemyQueueManager.Instance.OnAttack += DoFadeImage;

            _image = GetComponent<Image>();
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnElementDecided -= UpdateImage;
            EnemyQueueManager.Instance.OnAttack -= DoFadeImage;
        }

        private void UpdateImage(Element element)
        {
            _currentElementImage.sprite = element.Image;

            _currentElementImage.DOFade(1f, .3f);

            _image.enabled = false;

            _currentElementImage.transform.DOPunchScale(_scale, _duration, _vibration, _elasticity);
        }

        private void DoFadeImage()
        {
            _currentElementImage.DOFade(0f, .3f);
            _image.enabled = true;
        }
    }
}