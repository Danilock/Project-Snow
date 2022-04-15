using DG.Tweening;
using Game.Player;
using Game.Tween;
using TMPro;
using UnityEngine;

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

        private void UpdateHitUI(float amount)
        {
            _hitScore.text = $"x{((int)amount).ToString()}";
            Scale();
        }

        private void ShowHitText() => _group.DOFade(1f, .3f);

        private void HideHitText() => _group.DOFade(0f, .3f);
    }
}