using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class UIPlayerShieldButton : BaseButton
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            LevelManager.Instance.GetPlayer.GetPlayerShieldUsage.SetShieldState(true);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            LevelManager.Instance.GetPlayer.GetPlayerShieldUsage.SetShieldState(false);
        }
    }
}