using System.Linq;
using DefaultNamespace.Abstract_classes;
using Managers;
using UnityEngine;

namespace DefaultNamespace
{
    public class MagicsManager : MonoBehaviour, IDataReturner
    {
        private MagicSOManager[] _magics;
        private MagicSOManager _currentMagic;
        private int _currentIndex;
        private int _maxWeapons;
    
        private void Awake()
        {
            _magics = GetComponentsInChildren<MagicSOManager>();
            _currentMagic = _magics[0];
            _currentIndex = 0;
            _maxWeapons = _magics.Length;
        }

        private void Start()
        {
            MagicAttackSO currentMagic = null;
            if (GameStatsManager.Instance.MagicsAttackSO != null)
            {
                currentMagic = GameStatsManager.Instance.MagicsAttackSO.FirstOrDefault(
                    magic => magic.Name == _currentMagic.MagicData.Name);
            }
        
            if (currentMagic == null)
            {
                currentMagic = _currentMagic.MagicData;
                GameStatsManager.Instance.MagicsAttackSO?.Add(currentMagic);
            }
        
            GameStatsManager.Instance.SelectedMagic = currentMagic;
        }
        
        public void SetCurrent(int index)
        {
            _currentIndex = index;
            _currentMagic = _magics[_currentIndex];
            MagicAttackSO currentMagic = null;
            if (GameStatsManager.Instance.MagicsAttackSO != null)
            {
                currentMagic = GameStatsManager.Instance.MagicsAttackSO.FirstOrDefault(
                    magic => magic.Name == _currentMagic.MagicData.Name);
            }
        
            if (currentMagic == null)
            {
                Debug.Log("aaaaaa");
                currentMagic = _currentMagic.MagicData;
                GameStatsManager.Instance.MagicsAttackSO?.Add(currentMagic);
            }
            GameStatsManager.Instance.SelectedMagic = currentMagic;
        }

        public int GetCurrent()
        {
            return _currentIndex;
        }

        public int GetMaxCount()
        {
            return _maxWeapons;
        }
    }
}