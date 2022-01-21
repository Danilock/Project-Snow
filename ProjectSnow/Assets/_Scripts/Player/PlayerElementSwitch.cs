using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.AbilitySystem;
using Game.DamageSystem.Attacks;

namespace Game.Player
{
    [RequireComponent(typeof(Damageable))]
    public class PlayerElementSwitch : BaseAbility
    {
        [Header("Elements")] 
        [SerializeField] private List<Element> _elemets;

        private Attack _attack;
        private Damageable _playerDamageable;
        
        protected override void Awake()
        {
            base.Awake();
            
            _playerDamageable = GetComponent<Damageable>();
            _attack = GetComponent<Attack>();
        }

        private void Start()
        {
            PlayerInput.Instance.Actions.Player.SelectFirstElement.performed += SelectFirstElementOnperformed;
            PlayerInput.Instance.Actions.Player.SelectSecondElement.performed += SelectSecondElementOnperformed; 
            PlayerInput.Instance.Actions.Player.SelectThirdElement.performed += SelectThirdElementOnperformed; 
            
            SelectPlayerElementByIndex(0);
        }

        private void SelectFirstElementOnperformed(InputAction.CallbackContext obj)
        {
            SelectPlayerElementByIndex(0);
        }

        private void SelectSecondElementOnperformed(InputAction.CallbackContext obj)
        {
            SelectPlayerElementByIndex(1);
        }

        private void SelectThirdElementOnperformed(InputAction.CallbackContext obj)
        {
            SelectPlayerElementByIndex(2);
        }

        /// <summary>
        /// Sets one of the elements specified in the ElementList and assign it to the player damageable. 
        /// </summary>
        /// <param name="index"></param>
        public void SelectPlayerElementByIndex(int index)
        {
            if(!CanUse)
                return;
            
            _playerDamageable.ChangeElement(_elemets[index]);
            _attack.ChangeElement(_elemets[index]);
            _playerDamageable.Shield.ChangeElement(_elemets[index]);

            StartCoroutine(HandleCooldown_CO());
        }
    }
}