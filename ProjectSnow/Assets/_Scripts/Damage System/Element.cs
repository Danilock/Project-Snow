using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game.DamageSystem
{
    /// <summary>
    /// Representation of an element in game.
    /// </summary>
    [CreateAssetMenu(fileName = "Element", menuName = "Create Element", order = 0)]
    public class Element : ScriptableObject
    {
        public string Name;

        public List<Element> StrongAgainst, WeakAgainst;

        [Header("Element color representation, used in UI's."), FoldoutGroup("Art")]
        [ColorPalette] public Color Color;

        [PreviewField, FoldoutGroup("Art")]
        public Sprite Image;

        [PreviewField, FoldoutGroup("Art")] 
        public Sprite HealthBarInstanceImage;

        /// <summary>
        /// Checks if this element is counter of the one specified.
        /// </summary>
        /// <param name="counterOf"></param>
        /// <returns></returns>
        public bool IsCounterOf(Element counterOf)
        {
            if (StrongAgainst.Contains(counterOf))
                return true;
            else
                return false;
        }

        public bool IsWeakerThan(Element stronger)
        {
            if (WeakAgainst.Contains(stronger))
                return true;
            else
            {
                return false;
            }
        }
    }
}