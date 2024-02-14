using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.DialogSystem
{
    [CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObject/QuestWithDialogsSo ", order = 2)]
    public class QuestWithDialogsSo : ScriptableObject
    {
        [field: SerializeField]
        public List<DialogString> StartDialog;
        [field: SerializeField]
        public List<DialogString> RepeatQuest;
        [field: SerializeField]
        public List<DialogString> EndDialog;

        [field: SerializeField]
        public string ID_Quest;

    }
}