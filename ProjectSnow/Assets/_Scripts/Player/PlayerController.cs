using System;
using System.Collections;
using System.Collections.Generic;
using Game.AbilitySystem;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Private Fields

        private PlayerAttack _playerAttack;
        private PlayerHealth _playerHealth;
        private PlayerElementSwitch _playerElementSwitch;
        private PlayerShieldUsage _playerShieldUsage;
        private EnergySource _playerEnergySource;

        #endregion

        #region Public Fields

        public PlayerAttack GetPlayerAttack => _playerAttack;
        public PlayerHealth GetPlayerHealth => _playerHealth;
        public PlayerElementSwitch GetPlayerElementSwitch => _playerElementSwitch;
        public PlayerShieldUsage GetPlayerShieldUsage => _playerShieldUsage;
        public EnergySource GetPlayerEnergySource => _playerEnergySource;

        #endregion
        
        private void Awake()
        {
            _playerAttack = GetComponent<PlayerAttack>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerElementSwitch = GetComponent<PlayerElementSwitch>();
            _playerShieldUsage = GetComponent<PlayerShieldUsage>();
            _playerEnergySource = GetComponent<EnergySource>();
        }
    }
}