using DG.Tweening;
using Game.Player;
using Game.Tween;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game.UI
{
    public class UIHitStreak : DoScale
    {
        [SerializeField] private TMP_Text _hitScore;
        [SerializeField] private TMP_Text _phaseName;
        [SerializeField] private CanvasGroup _group;

        #region Phase Name Tween
        [SerializeField, FoldoutGroup("Phase Name Tween")]
        private Vector2 _punchScale;

        [SerializeField, FoldoutGroup("Phase Name Tween")]
        private float _durationPhaseTextTweening = .3f;

        private bool _isAlreadyTweening = false;
        #endregion

        private void Start()
        {
            PlayerHitStreak.OnHit += UpdateHitUI;
            PlayerHitStreak.StartHitStreak += ShowHitText;
            PlayerHitStreak.EndHitStreak += HideHitText;
            PlayerHitStreak.OnChangePhase += UpdateHitPhaseText;

            UpdateHitPhaseText(PlayerHitStreak.Instance.GetCurrentPhase);
        }

        private void OnDisable()
        {
            PlayerHitStreak.OnHit -= UpdateHitUI;
            PlayerHitStreak.StartHitStreak -= ShowHitText;
            PlayerHitStreak.EndHitStreak -= HideHitText;
            PlayerHitStreak.OnChangePhase -= UpdateHitPhaseText;
        }

        private void UpdateHitUI(KillingStreakData data)
        {
            _hitScore.text = $"x{((int)data.CurrentHitCount).ToString()}";

            Scale();
        }

        private void UpdateHitPhaseText(KillingStreakPhase phase)
        {
            if (!_isAlreadyTweening) 
            {
                _isAlreadyTweening = true;

                _phaseName.transform.DOPunchScale(_punchScale, _durationPhaseTextTweening).OnComplete(() =>
                {
                    _isAlreadyTweening = false;
                });
            }

            _phaseName.DOColor(phase.Color, _durationPhaseTextTweening);

            _phaseName.text = phase.StrakPhaseName;
        }

        private void ShowHitText() => _group.DOFade(1f, .3f);

        private void HideHitText()
        {
            _group.DOFade(0f, .3f).OnComplete(() =>
            {
                _phaseName.text = PlayerHitStreak.Instance.GetCurrentPhase.StrakPhaseName;
            });
        }
    }
}