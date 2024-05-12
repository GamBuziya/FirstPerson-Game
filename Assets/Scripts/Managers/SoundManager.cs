using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundsCollectionSO _collections;
    public static SoundManager Instance;

    private List<AudioSource> _audioSources; // Змінна для зберігання джерела звуку

    private void Awake()
    {
        Instance = this;
        _audioSources = new List<AudioSource>();
        for (int i = 0; i < 5; i++)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1;
            audioSource.minDistance = 1;
            audioSource.maxDistance = 5;
            _audioSources.Add(audioSource);
        }
        
    }
    
    public void MagicAttackSound(TypeMagicAttack _magicAttack, Vector3 position)
    {
        switch (_magicAttack)
        {
            case TypeMagicAttack.Electricity:
                PlaySound(_collections.ElectricityAttackSounds,position);
                break;
            case TypeMagicAttack.Venom:
                PlaySound(_collections.VenomAttackSounds,position);
                break;
            case TypeMagicAttack.Fire:
                PlaySound(_collections.FireAttackSounds,position);
                break;
            case TypeMagicAttack.Ice:
                PlaySound(_collections.IceAttackSounds,position);
                break;
        }
        
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

    private void PlaySound(AudioClip[] audioClips, Vector3 position)
    {
        if (audioClips.Length == 0) return;

        AudioSource audioSource = _audioSources[_audioSources.Count() - 1];

        foreach (var tempSource in _audioSources)
        {
            if (!tempSource.isPlaying)
            {
                audioSource = tempSource;
                break;
            }
        }

        var temp = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.volume = 0.05f;
        audioSource.transform.position = position;
        audioSource.PlayOneShot(temp);
    }
}
