using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Item
{
    public abstract override void Use();
    public abstract override void Aim();
    public abstract override void DeAim();

    public GameObject bulletImpactPrefab;
    public ParticleSystem ps;
    public Animator gunAnim;
    public AudioSource ac;
    public AudioSource ac2;
    
}
