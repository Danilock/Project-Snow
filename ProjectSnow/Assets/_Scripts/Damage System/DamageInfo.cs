using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DamageSystem
{
    /// <summary>
    /// Contains information about a damage Transmitter.
    /// </summary>
    public class DamageInfo
    {
        /// <summary>
        /// Who is causing this damage.
        /// </summary>
        public Damageable Transmitter;
        public float Damage;
        public bool IgnoreInvulnerability = false;


        private Element _element;

        public Element Element
        {
            get
            {
                if (_element == null)
                    return Transmitter.Element;

                return _element;
            }
            private set => _element = value;
        }
        public DamageInfo(){ }

        public DamageInfo(Damageable transmitter, float damage, bool ignoreInvulnerability)
        {
            this.Transmitter = transmitter;
            this.Damage = damage;
            this.IgnoreInvulnerability = ignoreInvulnerability;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transmitter"></param>
        /// <param name="damage"></param>
        /// <param name="ignoreInvulnerability"></param>
        /// <param name="element">Independent element of this DamageInfo. If it's null then we set the element of the transmitter.</param>
        public DamageInfo(Damageable transmitter, float damage, bool ignoreInvulnerability, Element element)
        {
            
            this.Transmitter = transmitter;
            this.Damage = damage;
            this.IgnoreInvulnerability = ignoreInvulnerability;
            this.Element = element;
        }
    }
}