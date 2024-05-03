using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoundsCollectionSO : ScriptableObject
{
    public AudioClip[] AttackSounds;
    public AudioClip[] BlockSounds;
    public AudioClip[] HitSounds;
    public AudioClip[] FireAttackSounds;
    public AudioClip[] IceAttackSounds;
    public AudioClip[] FleshAttackSounds;
}
