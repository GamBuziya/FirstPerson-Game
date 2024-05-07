using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundsCollectionSO _collections;
    public static SoundManager Instance;

    private AudioSource _audioSource; // Змінна для зберігання джерела звуку

    private void Awake()
    {
        Instance = this;
        _audioSource = gameObject.AddComponent<AudioSource>(); // Додаємо компонент AudioSource
        _audioSource.spatialBlend = 1;
        _audioSource.minDistance = 1;
        _audioSource.maxDistance = 5;
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
        _audioSource.Stop(); // Вимикаємо попередній звук
        PlaySound(_collections.HitSounds, gameObject.transform.position);
    }

    private void PlaySound(AudioClip[] audioClips, Vector3 position)
    {
        if (audioClips.Length == 0) return;

        var temp = audioClips[Random.Range(0, audioClips.Length)];
        _audioSource.volume = 0.05f;
        _audioSource.transform.position = position;
        _audioSource.PlayOneShot(temp);
    }
}
