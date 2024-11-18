using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    public static VFXController instance;
    [SerializeField] private List<ParticleSystem> effects;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySFX(int index,Vector3 playPosition)
    {
        ParticleSystem effect = Instantiate(effects[index], playPosition, Quaternion.identity);
        effect.Play();
    }
}
