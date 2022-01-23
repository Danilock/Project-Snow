using System.Collections;
using System.Collections.Generic;
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
    }
}