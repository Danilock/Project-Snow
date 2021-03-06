using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.DamageSystem
{
    [System.Serializable]
    public class Shield : IElemental
    {
        #region Private Fields
        [SerializeField] private float _shieldAmount;
        [SerializeField] private Element _element;
        [SerializeField, Header("Defines if this shield can be damaged")] private bool _canBreak = true;
        public Element Element => _element;
        public UnityAction OnTakeDamage;
        #endregion

        #region Constructors

        public  Shield() { }

        public Shield(Element element, float shieldAmount)
        {
            _element = element;
            _shieldAmount = shieldAmount;
        }

        #endregion
        
        #region Public fields
        public float ShieldAmount => _shieldAmount;

        public bool IsActive = true;

        #endregion

        #region Methods
        public void DamageShield(DamageInfo info)
        {
            if(!_canBreak)
                return;

            _shieldAmount -= DamageCalculations.CalculateDamageBasedInElements
                (
                    info.Damage, 
                    _element, 
                    info.Transmitter.Element
                );
            
            OnTakeDamage?.Invoke();
        }

        /// <summary>
        /// Sets an Element for the shield.
        /// </summary>
        /// <param name="element"></param>
        public void SetElement(Element element)
        {
            _element = element;
        }

        public void SetAmount(int value) => _shieldAmount = value;

        public void AddAmount(int value) => _shieldAmount += value;

        #endregion
        public void ChangeElement(Element newElement)
        {
            _element = newElement;
        }
    }
}