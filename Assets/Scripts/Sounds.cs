using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Sounds : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> _hitSound;
        [SerializeField] private List<AudioSource> _attackSound;
        [SerializeField] private AudioSource _mainMusic;

        public List<AudioSource> GeAttackSound() => _attackSound;
        public AudioSource GetMainMusic() => _mainMusic;

        public void PlayRandomAttackSound()
        {
            _attackSound[Random.Range(0, _attackSound.Count)].Play();
        }
        public void PlayRandomHitSound()
        {
            _hitSound[Random.Range(0, _hitSound.Count)].Play();
        }
    }
}