using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "New Dialogue Character", menuName = "Dialogue System/Dialogue Character")]
    public class DialogueCharacterSO : ScriptableObject
    {
        [SerializeField] private string characterName;
        [SerializeField] private Sprite charcaterPhoto;

        public string Name => characterName;
        public Sprite ProfilePhoto => charcaterPhoto;
    }
}
