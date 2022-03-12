using System.Collections;
using System.Collections.Generic;
using Codice.CM.Common.Serialization.Replication;
using Game.DamageSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Profile", order = 0, menuName = "Profiles/Enemy")]
    public class EnemyProfile : ScriptableObject
    {
        [FoldoutGroup("Health")] public List<EnemyHealthBar> HealthBars;

        #region Attack
        [FoldoutGroup("Attack")] public float Damage = 5f;
        [FoldoutGroup("Attack")] public float Cooldown = .5f;

        [FoldoutGroup("Attack")] public float MinCooldown = 3f, MaxCooldown = 6f;
        [FoldoutGroup("Attack")] public float SecondsInChargeAttackState = 2f;
        
        [FoldoutGroup("Attack")] public List<Element> PossibleElements;
        #endregion
        
        #region Art

        [PreviewField] public Sprite EnemySprite;
        public Vector3 BodyOffset;
        
        [FoldoutGroup("Scale On Hit")]
        public Vector3 TargetScaleSize = new Vector3(1.3f, 1.3f, 1.3f);
        [FoldoutGroup("Scale On Hit")]
        public float Duration = 1f;
        [FoldoutGroup("Scale On Hit")]
        public int Elasticity = 1;
        [FoldoutGroup("Scale On Hit")]
        public int Vibration = 25;

        #endregion
    }
}