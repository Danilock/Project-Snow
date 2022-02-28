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
            EnemyQueueManager.Instance.OnElementDecided += ActiveAttackView;
            EnemyQueueManager.Instance.OnAttack += ActiveGameView;
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnElementDecided -= ActiveAttackView;
            EnemyQueueManager.Instance.OnAttack -= ActiveGameView;
        }

        public void SetAttackCameraState(bool state) => _enemyAttackCamera.gameObject.SetActive(state);

        public void SetGameCameraState(bool state) => _gameCamera.gameObject.SetActive(state);

        public void ActiveAttackView(Element element)
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