using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.DialogSystem
{
    [CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObject/DialogsSo ", order = 2)]
    public class DialogsSO : ScriptableObject
    {
        [field: SerializeField]
        public List<DialogString> StartDialog;
        [field: SerializeField]
        public List<DialogString> RepeatQuest;
        [field: SerializeField]
        public List<DialogString> EndDialog;



    }
}