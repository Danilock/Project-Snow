using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.DamageSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.UI
{
    public class DamageHitUI : DamageUIElement
    {
        [SerializeField] private TMP_Text _tmpText;

        private Image _canvasGroup;

        private void Start()
        {
            _canvasGroup = GetComponent<Image>();
        }

        public override void Init(DamageInfo damageInfo)
        {
            _tmpText.text = damageInfo.Damage.ToString();

            _canvasGroup.DOFade(0f, .5f);
            transform.DOMoveY(transform.position.y + 5f, .5f).OnComplete(() => gameObject.SetActive(false));
        }
    }
}