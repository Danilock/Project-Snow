using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.AbilitySystem;
using Game.DamageSystem.Attacks;
using UnityEngine.Events;

namespace Game.Player
{
    [RequireComponent(typeof(Damageable))]
    public class PlayerElementSwitch : BaseAbility
    {
        [Header("Elements")] 
        [SerializeField] private List<Element> _elemets;

        public Element CurrentElement;
        public List<Element> GetPlayerElements => _elemets;
        public UnityAction<Element> OnElementChange;

        private Attack _attack;
        private Damageable _playerDamageable;
        
        protected override void Awake()
        {
            base.Awake();
            
            _playerDamageable = GetComponent<Damageable>();
            _attack = GetComponent<Attack>();
        }

        private void OnDisable()
        {
            PlayerInput.Instance.Actions.Player.SelectFirstElement.performed -= SelectFirstElementOnperformed;
            PlayerInput.Instance.Actions.Player.SelectSecondElement.performed -= SelectSecondElementOnperformed; 
            PlayerInput.Instance.Actions.Player.SelectThirdElement.performed -= SelectThirdElementOnperformed; 
        }

        private void Start()
        {   
            PlayerInput.Instance.Actions.Player.SelectFirstElement.performed += SelectFirstElementOnperformed;
            PlayerInput.Instance.Actions.Player.SelectSecondElement.performed += SelectSecondElementOnperformed; 
            PlayerInput.Instance.Actions.Player.SelectThirdElement.performed += SelectThirdElementOnperformed; 
            
            SelectPlayerElementByIndex(0);
            
            _playerDamageable.Shield.ChangeElement(_elemets[0]);
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

            CurrentElement = _elemets[index];
            
            _playerDamageable.ChangeElement(CurrentElement);
            _attack.ChangeElement(CurrentElement);
            
            OnElementChange?.Invoke(CurrentElement);

            StartCoroutine(HandleCooldown_CO());
        }

        public void SelectPlayerElement(Element element)
        {
            if (!CanUse)
                return;

            CurrentElement = element;
            
            _playerDamageable.ChangeElement(element);
            _attack.ChangeElement(element);
            
            OnElementChange?.Invoke(CurrentElement);

            StartCoroutine(HandleCooldown_CO());
        }
    }
}