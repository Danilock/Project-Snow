using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Game.UI
{
    public class BaseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {

        [FoldoutGroup("Tweening")]
        [SerializeField] private Vector3 _scaleOnMouseEnter = new Vector3(1.3f, 1.3f, 0f);

        [SerializeField, FoldoutGroup("Tweening")] private float _duration = .5f;

        /// <summary>
        /// Calculates the target scale once the mouse enters based on it's current scale.
        /// </summary>
        protected Vector3 ScaleOnMouseEnter
        {
            get
            {
                Vector3 currentScale = transform.localScale;
                Vector3 targetScale = new Vector3
                (Mathf.Sign(currentScale.x) * _scaleOnMouseEnter.x,
                    Mathf.Sign(currentScale.y) * _scaleOnMouseEnter.y,
                    Mathf.Sign(currentScale.z) * _scaleOnMouseEnter.z);

                return targetScale;
            }
        }
        
        protected Vector3 InitialScale;

        protected virtual void Start()
        {
            InitialScale = transform.localScale;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(ScaleOnMouseEnter, _duration);
        }


        public virtual void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(InitialScale, _duration);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            transform.DOScale(InitialScale, _duration);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}