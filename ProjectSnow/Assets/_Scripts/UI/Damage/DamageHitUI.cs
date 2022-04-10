using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.DamageSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.UI
{
    public class DamageHitUI : DamageUIElement
    {
        [SerializeField] private TMP_Text _tmpText;

        [FoldoutGroup("Tweening")] 
        [SerializeField]
        private Vector3 _additiveEndPosition = new Vector3(0f, 5f, 0f);

        [FoldoutGroup("Tweening")] [SerializeField, Header("Both fade and movement of this HIT UI Element")]
        private float _tweenDuration = .5f;
        private Image _canvasGroup;

        private void Start()
        {
            _canvasGroup = GetComponent<Image>();
        }

        public override void Init(DamageInfo damageInfo)
        {
            _tmpText.text = Mathf.Round(damageInfo.Damage).ToString();

            _canvasGroup.DOFade(0f, _tweenDuration);
            transform.DOMove(transform.position + _additiveEndPosition, _tweenDuration).OnComplete(() => gameObject.SetActive(false));
        }

        public void ShowMissText()
        {
            _tmpText.text = "MISS";

            _canvasGroup.DOFade(0f, _tweenDuration);
            transform.DOMove(transform.position + _additiveEndPosition, _tweenDuration).OnComplete(() => gameObject.SetActive(false));
        }
    }
}