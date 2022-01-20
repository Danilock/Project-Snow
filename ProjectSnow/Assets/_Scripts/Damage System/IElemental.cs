using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DamageSystem
{
    public interface IElemental
    {
        public Element Element { get; }
        public void ChangeElement(Element newElement);
    }
}
