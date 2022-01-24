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

        [Header("Element color representation, used in UI's.")]
        [ColorPalette] public Color Color;

        [PreviewField]
        public Sprite Image;
    }
}