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
    }
    
    public void MagicAttackSound(TypeMagicAttack _magicAttack, float volume = 1f)
    {
        switch (_magicAttack)
        {
            case TypeMagicAttack.Electricity:
                PlaySound(_collections.ElectricityAttackSounds, volume);
                break;
            case TypeMagicAttack.Venom:
                PlaySound(_collections.VenomAttackSounds, volume);
                break;
            case TypeMagicAttack.Fire:
                PlaySound(_collections.FireAttackSounds, volume);
                break;
            case TypeMagicAttack.Ice:
                PlaySound(_collections.IceAttackSounds, volume);
                break;
        }
        
    }

    public void AttackSound(GameObject gameObject)
    {
        PlaySound(_collections.AttackSounds);
    }
    
    public void BlockSound(GameObject gameObject)
    {
        PlaySound(_collections.BlockSounds);
    }
    
    public void HitSound(GameObject gameObject)
    {
        _audioSource.Stop(); // Вимикаємо попередній звук
        PlaySound(_collections.HitSounds);
    }

    private void PlaySound(AudioClip[] audioClips, float volume = 1f)
    {
        if (audioClips.Length == 0) return;

        var temp = audioClips[Random.Range(0, audioClips.Length)];
        _audioSource.PlayOneShot(temp, volume); // Відтворюємо звук один раз
    }
}
