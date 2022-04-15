using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;
using Game.DamageSystem;
using DG.Tweening;

namespace Shaders
{
    public class PlayerElementOutlineSelector : MonoBehaviour
    {
        [SerializeField] private PlayerElementSwitch _elementSwitch;
        private Material _material;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
        }

        private void OnEnable()
        {
            _elementSwitch.OnElementChange += UpdateShader;
        }

        private void OnDisable()
        {
            _elementSwitch.OnElementChange -= UpdateShader;
        }

        private void UpdateShader(Element element)
        {
            _material.DOColor(element.Color, "_OutlineColor", .3f);
            _material.DOColor(element.Color, "_AlphaOutlineColor", .3f);
            _material.DOColor(element.Color, "_InnerOutlineColor", .3f);
        }
    }
}