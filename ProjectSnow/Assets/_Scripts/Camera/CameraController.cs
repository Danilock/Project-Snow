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
            EnemyQueueManager.Instance.OnElementDecided += ActiveAttackViewOnElementDecided;
            EnemyQueueManager.Instance.OnAttack += ActiveGameView;
            EnemyQueueManager.Instance.OnChangeEnemy += ActiveGameViewOnChangeEnemy;
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnElementDecided -= ActiveAttackViewOnElementDecided;
            EnemyQueueManager.Instance.OnAttack -= ActiveGameView;
            EnemyQueueManager.Instance.OnChangeEnemy -= ActiveGameViewOnChangeEnemy;
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

        private void ActiveAttackViewOnElementDecided(Element element) => ActiveAttackView();

        private void ActiveGameViewOnChangeEnemy(EnemyHealth enemy) => ActiveGameView();
    }
}