using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Game.DamageSystem;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIPlayerHealth : MonoBehaviour
    {
        [SerializeField] private UIPlayerHealthInstance _healthInstancePrefab;
        [SerializeField, ReadOnly] private List<UIPlayerHealthInstance> _healthInstances;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
        [SerializeField, ReadOnly] private float _index;

        [FoldoutGroup("Tweening")]
        [SerializeField] private Vector2 _heartSizeOnDesactivate = new Vector2(1.7f, 1.7f);

        [FoldoutGroup("Tweening")]
        [SerializeField] private float _duration = .4f;

        // Start is called before the first frame update
        private void Start()
        {
            LevelManager.Instance.GetPlayer.GetPlayerHealth.OnTakeDamage += UpdateHealth;

            _index = LevelManager.Instance.GetPlayer.GetPlayerHealth.StartHealth;

            StartCoroutine(InitializeHearts());
        }

        private void OnDisable()
        {
            LevelManager.Instance.GetPlayer.GetPlayerHealth.OnTakeDamage -= UpdateHealth;
        }

        private void UpdateHealth(DamageInfo info)
        {
            _index -= info.Damage;

            for(int i = _healthInstances.Count - 1; i > _index - 1; i--)
            {
                _healthInstances[i].DesactivateHeart(_heartSizeOnDesactivate, _duration);
            }
        }

        private IEnumerator InitializeHearts()
        {
            for(int i = 0; i < LevelManager.Instance.GetPlayer.GetPlayerHealth.StartHealth; i++)
            {
                UIPlayerHealthInstance go = Instantiate(_healthInstancePrefab, _horizontalLayoutGroup.gameObject.transform);

                _healthInstances.Add(go);
            }

            yield return new WaitForEndOfFrame();

            _horizontalLayoutGroup.enabled = false;
        }
    }
}