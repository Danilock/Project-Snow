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
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class UIPlayerElementButton : BaseButton
    {
        [SerializeField] private Element _elemetToChoose;
        
        private enum Target
        {
            Attack,
            Shield
        }

        [SerializeField] private Target _target = Target.Attack;

        public void PickElement()
        {
            if(_target == Target.Attack)
                LevelManager.Instance.GetPlayer.GetPlayerElementSwitch.SelectPlayerElement(_elemetToChoose);
            else
                LevelManager.Instance.GetPlayer.GetPlayerHealth.Shield.ChangeElement(_elemetToChoose);
        }
    }
}