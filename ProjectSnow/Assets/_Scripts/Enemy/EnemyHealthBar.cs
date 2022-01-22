using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemy
{
    /// <summary>
    /// A healthbar is a fragment of enemy's life that will change enemies Element.
    /// </summary>
    [System.Serializable]
    public class EnemyHealthBar
    {
        [Range(0, 100), Tooltip("Representation of health's value in percentage")]
        public int Value;

        public Element Element;
    }
}