using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.DamageSystem;
using Game.Enemy;
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

        private bool _isEnabled = false;
        #endregion

        #region Public Fields
        
        public Image GetImage => _image;
        public EnemyHealthBar GetBar => _healthBar;
        
        #endregion
        
        public void Init(EnemyHealthBar healthBar)
        {
            _healthBar = healthBar;
            _image.color = Color.white;
        }

        public void UpdateBar()
        {
            float targetValue = _healthBar.CurrentValue / _healthBar.StartValue;

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
            
            _isEnabled = true;
        }
    }
}