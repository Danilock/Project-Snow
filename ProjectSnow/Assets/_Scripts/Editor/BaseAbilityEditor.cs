using System;
using System.Collections;
using System.Collections.Generic;
using Game.AbilitySystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Game.EditorScripts
{
    [CustomEditor(typeof(BaseAbility))]
    public class BaseAbilityEditor : Editor
    {
        private SerializedProperty _isSourceRequired, _requiredEnergy, _energySource;
        private void OnEnable()
        {
            _isSourceRequired = serializedObject.FindProperty("IsSourceRequired");
            _energySource = serializedObject.FindProperty("EnergySource");
            _requiredEnergy = serializedObject.FindProperty("RequiredEnergy");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_isSourceRequired);
            
            if (_isSourceRequired.boolValue == true)
            {
                EditorGUILayout.PropertyField(_energySource);
                EditorGUILayout.PropertyField((_requiredEnergy));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}