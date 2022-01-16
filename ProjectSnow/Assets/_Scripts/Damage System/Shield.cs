using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DamageSystem
{
    [System.Serializable]
    public class Shield
    {
        #region Private Fields
        [SerializeField] private int _shieldAmount;
        [SerializeField] private Element _element;
        #endregion

        #region Constructors

        public  Shield() { }

        public Shield(Element element, int shieldAmount)
        {
            _element = element;
            _shieldAmount = shieldAmount;
        }

        #endregion
        
        #region Public fields
        public int ShieldAmount => _shieldAmount;

        #endregion

        #region Methods
        public void DamageShield(DamageInfo info)
        {
            _shieldAmount -= DamageCalculations.CalculateDamageBasedInElements
                (
                    info.Damage, 
                    _element, 
                    info.Transmitter.Element
                );
        }

        /// <summary>
        /// Sets an Element for the shield.
        /// </summary>
        /// <param name="element"></param>
        public void SetElement(Element element)
        {
            _element = element;
        }

        public void SetShieldAmount(int value) => _shieldAmount = value;

        #endregion
    }
}