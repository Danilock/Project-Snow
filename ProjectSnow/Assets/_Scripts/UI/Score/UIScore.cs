using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game.Score;

namespace Game.UI
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        [SerializeField] private string _format = "0000000000";

        private void Start()
        {
            ScoreManager.Instance.OnScore += UpdateScoreText;
        }

        private void OnDisable()
        {
            ScoreManager.Instance.OnScore -= UpdateScoreText;
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
                return;

            _scoreText.text = _format;
        }

        private void UpdateScoreText(ScoreData data)
        {
            _scoreText.text = string.Format("{0:" + _format +"}", data.ScoreAmount);
        }
    }
}