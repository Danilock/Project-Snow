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

        public DamageInfo(){ }

        public DamageInfo(Damageable transmitter, float damage)
        {
            this.Transmitter = transmitter;
            this.Damage = damage;
        }
    }
}