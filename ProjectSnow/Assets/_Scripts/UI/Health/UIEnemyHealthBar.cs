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

        [SerializeField]
        private UIEnemyAttackElement _uIEnemyAttackElement;

        private List<HealthBarInstance> _instantiatedBars = new List<HealthBarInstance>();

        private EnemyHealth GetCurrentEnemyHealth => EnemyQueueManager.Instance.GetCurrentEnemy;
        private void Start()
        {
            EnemyQueueManager.Instance.OnChangeEnemy += SetupBars;
            EnemyQueueManager.Instance.OnElementDecided += UpdateCurrentBarImage;
            
            SetupBars(GetCurrentEnemyHealth);
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnChangeEnemy -= SetupBars;
            EnemyQueueManager.Instance.OnElementDecided -= UpdateCurrentBarImage;
        }

        private void SetupBars(EnemyHealth newEnemy)
        {
            //Deleting previous bars
            if (_instantiatedBars != null && _instantiatedBars.Count > 0)
                DeletePreviousBars();

            //Register to onhit event of the new enemy
            newEnemy.OnTakeDamage += UpdateCurrentBarSize;
            newEnemy.OnChangeBar += UpdateCurrentBarImage;

            //Instantiate new bars for the new enemy
            for (int i = 0; i < newEnemy.HealthBars.Count; i++)
            {
                HealthBarInstance instance = Instantiate(_healthInstancePrefab, _healthContainer.transform);
                instance.Init(newEnemy.HealthBars[i]);
                _instantiatedBars.Add(instance);

                if (i == 0)
                {
                    instance.EnableBar();
                }
            }
        }

        private void UpdateCurrentBarSize(DamageInfo info)
        {
            HealthBarInstance barInstance = FindUIBarByHealthBar();
            
            if(barInstance == null)
                return;

            barInstance.EnableBar();
            barInstance.UpdateBar();
        }

        private void UpdateCurrentBarImage(Element element)
        {
            HealthBarInstance barInstance = FindUIBarByHealthBar();

            if (barInstance == null)
                return;

            barInstance.ChangeBarInstanceImage(GetCurrentEnemyHealth.CurrentHealthBar.Element.HealthBarInstanceImage);
        }

        private void UpdateCurrentBarImage(EnemyHealthBar bar)
        {
            _uIEnemyAttackElement.UpdateImage(bar.Element);
        }

        private HealthBarInstance FindUIBarByHealthBar() => _instantiatedBars.Find(x => x.GetBar == GetCurrentEnemyHealth.CurrentHealthBar);

        private void DeletePreviousBars()
        {
            foreach(HealthBarInstance currentInstance in _instantiatedBars)
                Destroy(currentInstance.gameObject);
            
            _instantiatedBars.Clear();
        }
    }
}