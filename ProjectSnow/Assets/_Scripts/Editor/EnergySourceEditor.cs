using System;
using System.Collections;
using System.Collections.Generic;
using Game.AbilitySystem;
using UnityEditor;
using UnityEngine;

namespace Game.EditorScripts
{
    [CustomEditor(typeof(EnergySource))]
    public class EnergySourceEditor : Editor
    {
        private SerializedProperty _regenerationRate, _seconds, _maxEnergy, _currentEnergy, _canRegenerate, _isRegenerating;
        private void OnEnable()
        {
            _regenerationRate = serializedObject.FindProperty("RegenerationRate");
            _seconds = serializedObject.FindProperty("Seconds");
            _maxEnergy = serializedObject.FindProperty("MaxEnergy");
            _currentEnergy = serializedObject.FindProperty("CurrentEnergy");
            _canRegenerate = serializedObject.FindProperty("CanRegenerate");
            _isRegenerating = serializedObject.FindProperty("IsRegenerating");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Energy Regeneration Rate", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(_seconds);
            EditorGUILayout.PropertyField(_regenerationRate);
            
            EditorGUILayout.EndHorizontal();if(_seconds.floatValue > 5f)
                EditorGUILayout.HelpBox("Seconds are too high", MessageType.Warning);
            
            EditorGUILayout.LabelField("Regeneration Access", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(_canRegenerate);
            EditorGUILayout.PropertyField(_isRegenerating);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Energy", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(_maxEnergy);
            EditorGUILayout.PropertyField(_currentEnergy);
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }
}