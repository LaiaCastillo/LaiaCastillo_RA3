using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private RectTransform dialogBox;
        [SerializeField] private Image characterPhoto;
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private TextMeshProUGUI dialogueArea;
        public void ShowDialogueBox()
        {
            dialogBox.gameObject.SetActive(true);
        }

        public void HideDialogBox()
        {
            dialogBox.gameObject.SetActive(false);
        }

        public void SetCharacterInfo(DialogueCharacterSO charac)
        {
            if (charac == null) return;
            characterPhoto.sprite = charac.ProfilePhoto;
            characterName.text = charac.Name;
        }

        public void ClearDialogue()
        {
            dialogueArea.text = string.Empty;
        }

        public void SetDialogueArea(string text)
        {
            dialogueArea.text = text;
        }

        public void AppendToDialogueArea(char letter)
        {
            dialogueArea.text += letter;
        }
    }
}
