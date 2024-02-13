using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.DialogSystem
{
    [System.Serializable]
    public class DialogString
    {
        public string Text;
        public bool IsEnd;

        [Header("Branch")]
        public bool IsQuestion;

        public string AnswerOption1;
        public string AnswerOption2;
        public int option1Index;
        public int option2Index;

        [Header("Triggered Events")] 
        public UnityEvent StartDialogEvent;
        public UnityEvent EndDialogEvent;
    }
}