using UnityEngine;

namespace PlayerChoose
{
    public enum CharacterName
    {
        Richard,
        Cristina,
        Olivier,
        Worker
    }

    public class Character : MonoBehaviour
    {
        public bool IsSelected;
        public CharacterName Name;
    }
}