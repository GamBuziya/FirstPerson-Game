using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundsCollectionSO _collections;
    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AttackSound(GameObject gameObject)
    {
        PlaySound(_collections.AttackSounds, gameObject.transform.position);
    }
    
    public void BlockSound(GameObject gameObject)
    {
        PlaySound(_collections.BlockSounds, gameObject.transform.position);
    }
    
    public void HitSound(GameObject gameObject)
    {
        PlaySound(_collections.HitSounds, gameObject.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    
    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f)
    {
        var temp = audioClips[Random.Range(0, audioClips.Length)];
        AudioSource.PlayClipAtPoint(temp, position, volume);
    }
}
