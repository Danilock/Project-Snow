using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.DamageSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class DamageHitBarUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private Image _hitBarImage;
        [SerializeField] private Transform _oldPoolParent;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
            _hitBarImage = GetComponent<Image>();
        }

        public void Init(float targetLeft, float targetRight, Transform parent, Element element)
        {
            _oldPoolParent = transform.parent;

            transform.SetParent(parent);
            transform.localScale = new Vector3(1f, 1f, 1f);
            
            _transform.Reset();
            
            _transform.SetLeft(targetLeft);
            _transform.SetRight(targetRight);

            _hitBarImage.color = element.Color;

            _transform.DOMoveY(_transform.position.y - 1.5f, .3f).OnComplete(() =>
            {
                _hitBarImage.DOFade(0f, .2f).OnComplete(() =>
                {
                    transform.SetParent(_oldPoolParent);
                    
                    gameObject.SetActive(false);
                });
            });
        }
    }
}