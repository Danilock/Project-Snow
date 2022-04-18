using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.DamageSystem;
using DG.Tweening;
using ObjectPooling;

namespace Game.Enemy
{
    public class EnemyHitEffect : MonoBehaviour
    {
        [SerializeField] private List<Vector2> _positions;
        [SerializeField] private string _poolKey = "Hit Effect";

        private int _index = 0;

        private EnemyHealth _health;

        private void Awake()
        {
            _health = GetComponent<EnemyHealth>();
        }

        private void OnEnable()
        {
            _health.OnTakeDamage += InstantiateHitEffect;
        }

        private void OnDisable()
        {
            _health.OnTakeDamage -= InstantiateHitEffect;
        }

        private void InstantiateHitEffect(DamageInfo info)
        {
            GameObject _hitEffect = ObjectPooler.Instance.GetObjectFromPool(_poolKey);

            _hitEffect.transform.position = new Vector2(_positions[_index].x, _positions[_index].y);

            _hitEffect.gameObject.SetActive(true);

            _hitEffect.transform.DOScale(new Vector3(2f, 2f, 1f), .2f).OnComplete(() =>
            {
                _hitEffect.transform.DOScale(new Vector3(0f, 0f, 0f), .2f).OnComplete(() =>
               {
                   _hitEffect.transform.localScale = Vector3.zero;
                   _hitEffect.gameObject.SetActive(false);

                   _index = (_index + 1) % _positions.Count;
               });
            });
        }

        private void OnDrawGizmos()
        {
            for(int i = 0; i < _positions.Count; i++)
            {
                Gizmos.DrawWireSphere((Vector2)transform.position + _positions[i], .3f);
            }
        }
    }
}