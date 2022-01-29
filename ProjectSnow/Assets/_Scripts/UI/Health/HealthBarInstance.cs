using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Enemy;
using ObjectPooling;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// A segment of an enemy's health bar.
    /// </summary>
    public class HealthBarInstance : MonoBehaviour
    {
        #region Private Fields
        [SerializeField] private Image _image;
        [SerializeField] private Image _elementIconImage;
        [SerializeField] private EnemyHealthBar _healthBar;
        [SerializeField] private Text _text;
        [Header("Scale Sizer When Selected")] private float _scale = .3f;

        [Header("Pool")] [SerializeField] private string _pool;
        
        private bool _isEnabled = false;

        private RectTransform _rect;
        #endregion

        #region Public Fields
        
        public Image GetImage => _image;
        public EnemyHealthBar GetBar => _healthBar;
        
        #endregion
        
        public void Init(EnemyHealthBar healthBar)
        {
            _healthBar = healthBar;
            _image.color = Color.white;
            _rect = GetComponent<RectTransform>();
        }

        public void UpdateBar()
        {
            float targetValue = _healthBar.CurrentValue / _healthBar.StartValue;

            GenerateDamageBar(targetValue, _image.fillAmount);
            
            DOTween.To(() => _image.fillAmount, x => _image.fillAmount = x, targetValue, .3f).OnComplete(() =>
            {
                if (_healthBar.CurrentValue <= 0)
                {
                    DOTween.To(() => _image.fillAmount, x => _image.fillAmount = x, 0f, .1f);
                    
                    _text.DOFade(0f, .3f);
                    _elementIconImage.DOFade(0f, .3f);

                }
            });
            
            if(_isEnabled)
                _text.text = _healthBar.CurrentValue > 0 ? _healthBar.CurrentValue.ToString() : "0";
        }

        private void GenerateDamageBar(float targetValue, float currentImageFillAmount)
        {
            DamageHitBarUI bar = ObjectPooler.Instance.GetObjectFromPool(_pool).GetComponent<DamageHitBarUI>();

            float right = CalculateRectRight(targetValue, currentImageFillAmount);
            float left = CalculateRectLeft(targetValue, currentImageFillAmount);
            Debug.Log($"{left} --- {right}");
            bar.Init(left, right, transform, _healthBar.Element);
        }

        private float CalculateRectRight(float targetValue, float currentImageFillAmount)
        {
            float target = (1 - currentImageFillAmount) * _rect.GetRight();

            return target;
        }

        private float CalculateRectLeft(float targetValue, float currentImageFillAmount)
        {
            return targetValue * _rect.GetRight();
        }

        /// <summary>
        /// Called once when the bar is enabled by the UIEnemyHealthBar.cs
        /// </summary>
        public void EnableBar()
        {
            if(_isEnabled)
                return;

            _text.text = _healthBar.CurrentValue.ToString();
            _text.DOFade(1f, 1f);

            _image.DOColor(_healthBar.Element.Color, .5f);

            _elementIconImage.color = _healthBar.Element.Color;
            _elementIconImage.sprite = _healthBar.Element.Image;

            IncreaseBarSize();
            
            _isEnabled = true;
        }

        private void IncreaseBarSize()
        {
            _image.transform.DOScaleY(transform.localScale.y + _scale, .5f);
        }
    }
}