using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Game.DamageSystem
{
    /// <summary>
    /// Base class for damage calculations such as element dependencies.
    /// </summary>
    public class DamageCalculations : PersistentSingleton<DamageCalculations>
    {
        [SerializeField, Range(0, 100), Header("Amount of damage increased when a counter attacks a weaker")] private float _strongDamage = 50;
        
        [SerializeField, Range(0,100), Header("Amount of damage decreased when a weaker attacks a counter")] private float _weakerDamage = 25;
        
        /// <summary>
        /// Calculate damage based in the elements of the two objects. Adding or removing damage to the transmitter.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static int CalculateDamageBasedInElements(int damageAmount, Element receiver, Element transmitter)
        {
            if (receiver == null || transmitter == null)
                return damageAmount;

            int damageResult = damageAmount;

            //If the transmitter has a weakest element then we increase the damage.
            if (transmitter.StrongAgainst.Contains(receiver))
                damageResult += (int) (damageResult * (_instance._weakerDamage / 100));

            //Otherwise, we decrease the damage.
            else if (transmitter.WeakAgainst.Contains(receiver))
                damageResult -= (int) (damageResult * (_instance._strongDamage / 100));

            return damageResult;
        }
    }
}