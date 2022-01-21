using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Game.AbilitySystem
{
    /// <summary>
    /// Base class for abilities.
    /// </summary>
    public class BaseAbility : MonoBehaviour
    {
        [FoldoutGroup("Cooldown")] 
        [SerializeField] protected float Cooldown = .5f;
        [FoldoutGroup("Cooldown")]
        [SerializeField] protected bool CanUse = true;

        /// <summary>
        /// Can use ability taking in count mana.
        /// </summary>
        public bool CanUseAbility
        {
            get
            {
                if (IsSourceRequired)
                {
                    if (EnergySource.GetCurrentEnergy < RequiredEnergy)
                        return false;
                    else
                        return true;
                }
                else
                {
                    return CanUse;
                }
            }
            protected set
            {
                CanUse = value;
            }
        }

        [FoldoutGroup("Energy")] 
        [SerializeField] protected bool IsSourceRequired = true;
        [FoldoutGroup("Energy")]
        [SerializeField] protected EnergySource EnergySource;
        [FoldoutGroup("Energy")]
        [SerializeField] protected float RequiredEnergy;

        protected IEnumerator HandleCooldownCoroutine;

        /// <summary>
        /// An event used for other classes to know when this ability has been executed.
        /// </summary>
        public UnityAction OnAbilityUse;

        protected virtual void Awake()
        {
            HandleCooldownCoroutine = HandleCooldown_CO();
            
            if(EnergySource == null)
                EnergySource = GetComponent<EnergySource>();
        }

        /// <summary>
        /// Execute ability's behavior.
        /// </summary>
        public virtual void TriggerAbility()
        {
            if(!CanUseAbility)
                return;

            OnAbilityUse?.Invoke();
            
            EnergySource.UseEnergy(RequiredEnergy);

            StartCoroutine(HandleCooldownCoroutine);
        }

        protected virtual IEnumerator HandleCooldown_CO()
        {
            CanUse = false;
            yield return new WaitForSeconds(Cooldown);
            CanUse = true;
        }
    }
}