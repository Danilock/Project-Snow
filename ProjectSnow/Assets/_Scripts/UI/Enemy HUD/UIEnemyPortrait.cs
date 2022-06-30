using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace Game.UI
{
    public class UIEnemyPortrait : MonoBehaviour
    {
        [SerializeField] private Image _portrait;

        [ReadOnly] public UIEnemyPortrait Previous, Next;

        public void Init(Sprite portrait)
        {
            _portrait.sprite = portrait;
        }

        public void Move(Vector3 position, float duration = .3f)
        {
            Vector3 initialPosition = transform.position;

            transform.DOMove(position, duration).OnComplete(() =>
            {
                if(Next != null)
                    Next.Move(initialPosition);
            });
        }
    }
}