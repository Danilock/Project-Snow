using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class DamageHitBarUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
        }

        public void Init(float targetSize, float targetScale, Transform parent)
        {
            transform.SetParent(parent);
            
            transform.localScale = new Vector3(targetScale, 1f, 1f);
            
            _transform.Reset();
            
            _transform.SetLeft(targetSize);
            _transform.SetRight(-targetSize);
        }
    }
}