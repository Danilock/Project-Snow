using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using Game.Enemy;
using Managers;
using System.Linq;
using UnityEngine;

namespace Game.UI
{
    public class UIEnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject _healthContainer;
        [SerializeField] private HealthBarInstance _healthInstancePrefab;

        private List<HealthBarInstance> _instantiatedBars = new List<HealthBarInstance>();

        private EnemyHealth GetCurrentEnemyHealth => EnemyQueueManager.Instance.GetCurrentEnemy;
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(.3f);

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
            newEnemy.OnDeath += UpdateCurrentBar;

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