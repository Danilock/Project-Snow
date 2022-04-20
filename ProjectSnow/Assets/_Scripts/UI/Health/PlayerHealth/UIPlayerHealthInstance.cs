using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIPlayerHealthInstance : MonoBehaviour
    {
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        public void DesactivateHeart(Vector2 size, float duration)
        {
            _image.DOFade(0f, duration);
            transform.DOScale(size, duration);
        }
    }
}