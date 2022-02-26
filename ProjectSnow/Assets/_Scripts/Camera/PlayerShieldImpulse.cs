using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Camera
{
    public class PlayerShieldImpulse : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource _impulseSource;

        [ContextMenu("Generate Impulse")]
        public void GenerateImpulse()
        {
            _impulseSource.GenerateImpulse();
        }
    }
}