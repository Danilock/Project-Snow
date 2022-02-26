using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIPlayerShieldButton : BaseButton
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
            _button.onClick.AddListener(UsePlayerShield);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(UsePlayerShield);
        }

        private void UsePlayerShield()
        {
            LevelManager.Instance.GetPlayer.GetPlayerShieldUsage.TriggerAbility();
        }
    }
}