using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// Base class for UI elements displaying damage information.
    /// </summary>
    public abstract class DamageUIElement : UIElement
    {
        public abstract void Init(DamageInfo damageInfo);
    }
}