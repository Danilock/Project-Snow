using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerInput : PersistentSingleton<PlayerInput>
    {
        public PlayerActionsData Actions
        {
            get;
            private set;
        }

        protected override void Awake()
        {
            base.Awake();

            Actions = new PlayerActionsData();
            
            Actions.Enable();
        }

        public bool AttackKeyPressed => Actions.Player.Attack.IsPressed();
    }
}