using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Game.DamageSystem;
using Game.Enemy;
using Managers;
using Sirenix.OdinInspector;

namespace Game.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Cameras")] private CinemachineVirtualCamera _gameCamera;
        [SerializeField, FoldoutGroup("Cameras")] private CinemachineVirtualCamera _enemyAttackCamera;
        
        private void Start()
        {
            EnemyQueueManager.Instance.OnChangeEnemy += RegisterCallbacks;
            
            _enemyAttackCamera.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnChangeEnemy -= RegisterCallbacks;
        }

        private void RegisterCallbacks(EnemyHealth enemy)
        {
            EnemyAttack attack = enemy.GetComponent<EnemyAttack>();
            
            if(attack == null)
                return;

            attack.OnDecideElement += arg0 =>
            {
                ActiveAttackView();
            };

            attack.OnAttack += () =>
            {
                ActiveGameView();
            };
        }

        public void SetAttackCameraState(bool state) => _enemyAttackCamera.gameObject.SetActive(state);

        public void SetGameCameraState(bool state) => _gameCamera.gameObject.SetActive(state);

        public void ActiveAttackView()
        {
            SetGameCameraState(false);
            SetAttackCameraState(true);
        }

        public void ActiveGameView()
        {
            SetGameCameraState(true);
            SetAttackCameraState(false);
        }
    }
}