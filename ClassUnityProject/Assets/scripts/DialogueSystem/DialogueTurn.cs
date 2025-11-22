using UnityEngine;

namespace DialogueSystem
{
    [System.Serializable ]
    public class DialogueTurn 
    {
        [field: SerializeField] public DialogueCharacterSO character;
        [SerializeField, TextArea(2, 4)] private string dialogueLine = string.Empty;
        public string DialogueLine => dialogueLine;
    }
}
