using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem
{
    /// <summary>
    /// Contains information about a damage Transmitter.
    /// </summary>
    public struct DamageInfo
    {
        /// <summary>
        /// Who is causing this damage.
        /// </summary>
        public Damageable Transmitter;
        public int Damage;
    }
}