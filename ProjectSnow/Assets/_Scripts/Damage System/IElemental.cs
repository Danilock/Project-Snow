using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DamageSystem
{
    public interface IElemental
    {
        public Element Element { get; }
        /// <summary>
        /// Changes the element of this IElemental Entity.
        /// </summary>
        /// <param name="newElement"></param>
        public void ChangeElement(Element newElement);
    }
}
