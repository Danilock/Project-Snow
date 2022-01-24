using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Game.DamageSystem;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIElement : MonoBehaviour
    {
        [SerializeField, ReadOnly] private Element _currentElement;
        [SerializeField] private Image _image;

        public Element GetElement => _currentElement;

        public void Init(Element element)
        {
            _currentElement = element;

            _image.color = _currentElement.Color;
            _image.sprite = _currentElement.Image;
        }
    }
}