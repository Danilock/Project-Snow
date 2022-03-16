using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.DamageSystem;
using Game.Enemy;
using Game.Player;
using Game.Tween;
using Managers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI
{
    public class UIHitStreak : DoScale
    {
        [SerializeField] private TMP_Text _hitScore;
        [SerializeField] private CanvasGroup _group;
        private void Start()
        {
            PlayerHitStreak.OnHit += UpdateHitUI;
            PlayerHitStreak.StartHitStreak += ShowHitText;
            PlayerHitStreak.EndHitStreak += HideHitText;
        }

        private void OnDisable()
        {
            PlayerHitStreak.OnHit -= UpdateHitUI;
            PlayerHitStreak.StartHitStreak -= ShowHitText;
            PlayerHitStreak.EndHitStreak -= HideHitText;
        }

        private void UpdateHitUI(int amount)
        {
            _hitScore.text = $"x{amount.ToString()}";
            Scale();
        }

        private void ShowHitText() => _group.DOFade(1f, .3f);

        private void HideHitText() => _group.DOFade(0f, .3f);
    }
}