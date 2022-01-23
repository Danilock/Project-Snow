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

        private EnemyHealth GetEnemyInScene => EnemyQueueManager.Instance.GetCurrentEnemy;

        private EnemyElementChange GetEnemyElementChange;
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(.3f);

            EnemyQueueManager.Instance.OnChangeEnemy += SetupBars;
            
            SetupBars(GetEnemyInScene);
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
            
            GetEnemyElementChange = newEnemy.GetComponent<EnemyElementChange>();
            
            //Register to onhit event of the new enemy
            newEnemy.OnTakeDamage += OnTakeDamage;
            newEnemy.OnDeath -= OnTakeDamage;

            //Instantiate new bars for the new enemy
            foreach (EnemyHealthBar bar in GetEnemyElementChange.GetHealthBars)
            {
                HealthBarInstance instance = Instantiate(_healthInstancePrefab, _healthContainer.transform);
                instance.Init(bar);
                _instantiatedBars.Add(instance);
            }
        }

        private void OnTakeDamage(DamageInfo info)
        {
            HealthBarInstance barInstance =
                _instantiatedBars.Find(x => x.GetBar == GetEnemyElementChange.GetCurrentHealthBar);
            
            Debug.Log(barInstance.GetBar.Element.Name);
        }

        private void DeletePreviousBars()
        {
            foreach(HealthBarInstance currentInstance in _instantiatedBars)
                Destroy(currentInstance.gameObject);
            
            _instantiatedBars.Clear();
        }
    }
}