using System;
using System.Collections;
using System.Collections.Generic;
using Game.UI;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIPlayerAttackButton : BaseButton
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            if (_button == null)
                _button = GetComponent<Button>();
        }

        protected override void Start()
        {
            base.Start();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(DoPlayerAttack);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(DoPlayerAttack);
        }

        public void DoPlayerAttack()
        {
            LevelManager.Instance.GetPlayer.GetPlayerAttack.TriggerAbility();
        }
    }
}