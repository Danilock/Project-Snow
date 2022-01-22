using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game.AbilitySystem
{
    public class EnergySource : MonoBehaviour
    {
        [FoldoutGroup("Regeneration")]
        [SerializeField] protected float RegenerationRate, Seconds;
        
        [FoldoutGroup("Regeneration")]
        public bool CanRegenerate, IsRegenerating;

        [FoldoutGroup("Energy Amount")] [SerializeField]
        protected float MaxEnergy;
        [FoldoutGroup("Energy Amount"), ReadOnly, SerializeField] protected float CurrentEnergy;
        private IEnumerator RegenerateEnergyCoroutine;
        public float GetCurrentEnergy => CurrentEnergy;


        protected void Start()
        {
            CurrentEnergy = MaxEnergy;

            RegenerateEnergyCoroutine = RegenerateEnergy_CO();
        }

        protected void OnValidate()
        {
            if (CurrentEnergy > MaxEnergy)
                CurrentEnergy = MaxEnergy;
        }

        /// <summary>
        /// Adds energy to it's current value.
        /// </summary>
        /// <param name="valueToAdd"></param>
        public virtual void AddEnergy(float valueToAdd)
        {
            if (CurrentEnergy + valueToAdd > MaxEnergy)
                CurrentEnergy = MaxEnergy;
            else
                CurrentEnergy += valueToAdd;
        }

        /// <summary>
        /// Consumes energy.
        /// </summary>
        /// <param name="amount"></param>
        public virtual void UseEnergy(float amount)
        {
            if (CurrentEnergy - amount < 0)
                CurrentEnergy = 0;
            else
                CurrentEnergy -= amount;

            if(!IsRegenerating && CanRegenerate)
                StartCoroutine(RegenerateEnergyCoroutine);
        }

        protected IEnumerator RegenerateEnergy_CO()
        {
            IsRegenerating = true;
            
            while (CurrentEnergy <= MaxEnergy)
            {
                yield return new WaitForSeconds(Seconds);
                CurrentEnergy += RegenerationRate;

                if (CurrentEnergy > MaxEnergy)
                {
                    CurrentEnergy = MaxEnergy;
                    IsRegenerating = false;
                }
            }
        }

        public virtual void SetMaxEnergy(float newValue) => MaxEnergy = newValue;
    }
}