using System.Collections;
using System.Collections.Generic;
using Game.Player;
using UnityEngine;

namespace Managers
{
    public class LevelManager : SceneSingleton<LevelManager>
    {
        [SerializeField] private PlayerController _player;
        public PlayerController GetPlayer => _player;

        protected override void Awake()
        {
            base.Awake();

            if(_player == null)
                _player = FindObjectOfType<PlayerController>();
        }
    }
}