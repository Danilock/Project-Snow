using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using Game.Enemy;
using Managers;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIEnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject _healthContainer;
        [SerializeField] private HealthBarInstance _healthInstancePrefab;
        
        [SerializeField] private Image _currentElementImage;

        [FoldoutGroup("Tweening")] [SerializeField]
        private Vector3 _punchCurrentElementImage;

        [FoldoutGroup("Tweening"), SerializeField] private float _duration = 1f;
        [FoldoutGroup("Tweening"), SerializeField] private int _vibration = 10;
        [FoldoutGroup("Tweening"), SerializeField] private float _elasticity = 0f;

        private List<HealthBarInstance> _instantiatedBars = new List<HealthBarInstance>();

        private EnemyHealth GetCurrentEnemyHealth => EnemyQueueManager.Instance.GetCurrentEnemy;
        private void Start()
        {
            EnemyQueueManager.Instance.OnChangeEnemy += SetupBars;
            
            SetupBars(GetCurrentEnemyHealth);
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnChangeEnemy -= SetupBars;
        }

        private void SetupBars(EnemyHealth newEnemy)
        {
            //Deleting previous bars
            if (_instantiatedBars != null && _instantiatedBars.Count > 0)
                DeletePreviousBars();

            //Register to onhit event of the new enemy
            newEnemy.OnTakeDamage += UpdateCurrentBar;
            newEnemy.OnChangeBar += SetupBarElementImage;

            //Instantiate new bars for the new enemy
            for (int i = 0; i < newEnemy.HealthBars.Count; i++)
            {
                HealthBarInstance instance = Instantiate(_healthInstancePrefab, _healthContainer.transform);
                instance.Init(newEnemy.HealthBars[i]);
                _instantiatedBars.Add(instance);

                if (i == 0)
                {
                    instance.EnableBar();
                    SetupBarElementImage(newEnemy.CurrentHealthBar);
                }
            }
        }

        private void SetupBarElementImage(EnemyHealthBar newBar)
        {
            _currentElementImage.sprite = newBar.Element.Image;

            _currentElementImage.transform.DOPunchScale(_punchCurrentElementImage, _duration, _vibration, _elasticity);
        }

        private void UpdateCurrentBar(DamageInfo info)
        {
            HealthBarInstance barInstance =
                _instantiatedBars.Find(x => x.GetBar == GetCurrentEnemyHealth.CurrentHealthBar);
            
            if(barInstance == null)
                return;

            barInstance.EnableBar();
            barInstance.UpdateBar();
        }

        private void DeletePreviousBars()
        {
            foreach(HealthBarInstance currentInstance in _instantiatedBars)
                Destroy(currentInstance.gameObject);
            
            _instantiatedBars.Clear();
        }
    }
}