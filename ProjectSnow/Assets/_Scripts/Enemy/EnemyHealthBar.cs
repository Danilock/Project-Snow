using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Events;

namespace Game.Enemy
{
    /// <summary>
    /// A healthbar is a fragment of enemy's life that will change enemies Element.
    /// </summary>
    [System.Serializable]
    public class EnemyHealthBar
    {
        [FormerlySerializedAs("Value")] [Range(0, 10000)]
        public float StartValue;

        [ReadOnly] public float CurrentValue;

        public Element Element;

        [ReadOnly] public bool IsLastHealthBar = false;

        /// <summary>
        /// Once the bar's health turns to zero.
        /// </summary>
        public UnityAction OnDestroybar;

        public EnemyHealthBar() { }

        public EnemyHealthBar(float startValue, Element element)
        {
            StartValue = startValue;
            Element = element;
        }
    }
}