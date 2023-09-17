using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IswalkingBro : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystems;

    private void OnEnable()
    {
        particleSystems.Play();
    }
}
