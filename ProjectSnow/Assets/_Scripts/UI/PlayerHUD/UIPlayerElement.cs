using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using Managers;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using System.Linq;
using DG.Tweening;

namespace Game.UI
{
    public class UIPlayerElement : MonoBehaviour
    {
        [SerializeField] private UIElement _prefab;
        [SerializeField, ReadOnly] private List<UIElement> _elementsInstantiated;
        [SerializeField] private GameObject _horizontalLayout;
        [SerializeField] private GameObject _selector;
        
        private void Start()
        {
            InitializeElementLayouts();
            
            LevelManager.Instance.GetPlayer.GetPlayerElementSwitch.OnElementChange += OnElementChange;
        }

        /// <summary>
        /// Spawns the amount of elements the player has in the UI.
        /// </summary>
        private void InitializeElementLayouts()
        {
            foreach (Element currentElement in LevelManager.Instance.GetPlayer.GetPlayerElementSwitch.GetPlayerElements)
            {
                UIElement elementInstance = Instantiate(_prefab, _horizontalLayout.transform);
                
                elementInstance.Init(currentElement);
                
                _elementsInstantiated.Add(elementInstance);
            }
            
            _selector.transform.DOMove(_elementsInstantiated[0].transform.position, .4f).OnComplete(() =>
            {
                if (_selector.transform.position != _elementsInstantiated[0].transform.position)
                    _selector.transform.DOMove(_elementsInstantiated[0].transform.position, .4f);
            });
        }

        private void OnElementChange(Element newElement)
        {
            UIElement ue = _elementsInstantiated.Find(x => x.GetElement == newElement);
            
            if(ue == null)
                return;

            _selector.transform.DOMove(ue.transform.position, .4f).OnComplete(() =>
            {
                if (_selector.transform.position != ue.transform.position)
                    _selector.transform.DOMove(ue.transform.position, .4f);
            });
        }
    }
}