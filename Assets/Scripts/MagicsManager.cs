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
            GameStatsManager.Instance.SelectedMagic = _currentMagic.MagicData;
        }
    
        public void SetCurrent(int index)
        {
            _currentIndex = index;
            _currentMagic = _magics[_currentIndex];
            GameStatsManager.Instance.SelectedMagic = _currentMagic.MagicData; 
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