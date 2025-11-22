using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/ Dialogue Round")]
    public class DialogueRoundSO : ScriptableObject
    {
        [SerializeField] private List<DialogueTurn> dialogueTurnList;
        public List<DialogueTurn> DialogueTurnList => dialogueTurnList;
    }
}
