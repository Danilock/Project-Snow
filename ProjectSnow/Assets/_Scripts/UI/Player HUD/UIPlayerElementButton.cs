using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using Managers;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class UIPlayerElementButton : BaseButton
    {
        [SerializeField] private Element _elemetToChoose;

        [SerializeField] private Target _target = Target.Attack;

        [SerializeField] private UIElementCursor _cursor;

        public Element GetElement => _elemetToChoose;

        public void PickElement()
        {
            if(_target == Target.Attack)
                LevelManager.Instance.GetPlayer.GetPlayerElementSwitch.SelectPlayerElement(_elemetToChoose);
            else if(_target == Target.Shield)
                LevelManager.Instance.GetPlayer.GetPlayerHealth.Shield.ChangeElement(_elemetToChoose);
            else if(_target == Target.Both)
            {
                LevelManager.Instance.GetPlayer.GetPlayerElementSwitch.SelectPlayerElement(_elemetToChoose);
                LevelManager.Instance.GetPlayer.GetPlayerHealth.Shield.ChangeElement(_elemetToChoose);
            }

            _cursor.SetCursor(this);
        }
    }
    public enum Target
    {
        Attack,
        Shield,
        Both
    }
}