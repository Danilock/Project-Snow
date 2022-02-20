using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Game.UI
{
    public class BaseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {

        [FoldoutGroup("Tweening")]
        [SerializeField] private Vector3 _scaleOnMouseEnter = new Vector3(1.3f, 1.3f, 0f);

        [SerializeField, FoldoutGroup("Tweening")] private float _duration = .5f;
        
        private Vector3 _initialScale;

        protected virtual void Start()
        {
            _initialScale = transform.localScale;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(_scaleOnMouseEnter, _duration);
        }


        public virtual void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(_initialScale, _duration);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            transform.DOScale(_initialScale, _duration);
        }
    }
}