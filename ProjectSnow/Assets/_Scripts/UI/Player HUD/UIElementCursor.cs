using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.DamageSystem;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Game.UI
{
    public class UIElementCursor : MonoBehaviour
    {
        [SerializeField] private Image Image;

        [SerializeField] private Target _target;

        public void SetCursor(UIPlayerElementButton button)
        {
            Image.transform.DOMove(button.transform.position, .1f).OnComplete(() =>
            {
                Image.color = button.GetElement.Color;
                Image.transform.position = button.transform.position;
            });
        }
    }
}