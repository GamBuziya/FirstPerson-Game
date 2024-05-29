using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class BasicLevelLoader : MonoBehaviour
    {
        [SerializeField] protected Animator _transition;
        [SerializeField] protected float _transitionTime = 1f;
        
        
        public void LoadSceneByIndex(int index)
        {
            StartCoroutine(LoadScene(index));
        }
        
        public void RestartLevel()
        {
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
        

        IEnumerator LoadScene(int index)
        {
            _transition.SetTrigger("Start");
            yield return new WaitForSecondsRealtime(_transitionTime);
            
            DOTween.KillAll();
            Time.timeScale = 1f;
            SceneManager.LoadScene(index);
        }
    }
}