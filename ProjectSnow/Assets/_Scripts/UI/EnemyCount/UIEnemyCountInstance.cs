using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Game.UI
{
    public class UIEnemyCountInstance : MonoBehaviour
    {
        public UIEnemyCountInstance PreviousCount;
        public UIEnemyCountInstance NextCount;
        public TMP_Text TMProText;

        public void SetSize(Vector3 newSize, float duration)
        {
            transform.DOScale(newSize, duration);
        }
    }
}