using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Abilities
{
    /// <summary>
    /// Base class for abilities.
    /// </summary>
    public class BaseAbility : MonoBehaviour
    {
        [Header("Cooldown")] 
        [SerializeField] protected float Cooldown = .5f;
        [SerializeField] protected bool CanUse = true;

        protected IEnumerator HandleCooldownCoroutine;

        /// <summary>
        /// An event used for other classes to know when this ability has been executed.
        /// </summary>
        public UnityAction OnAbilityUse;

        protected virtual void Awake()
        {
            HandleCooldownCoroutine = HandleCooldown_CO();
        }

        /// <summary>
        /// Execute ability's behavior.
        /// </summary>
        public virtual void TriggerAbility()
        {
            if(!CanUse)
                return;
            
            OnAbilityUse.Invoke();

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