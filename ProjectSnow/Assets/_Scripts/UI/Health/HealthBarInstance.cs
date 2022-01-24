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
            _image.color = healthBar.Element.Color;
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
                }
            });

            _text.text = _healthBar.CurrentValue.ToString();
        }

        public void EnableBar()
        {
            if(_isEnabled)
                return;
            
            _text.DOFade(1f, 1f);

            _isEnabled = true;
        }
    }
}