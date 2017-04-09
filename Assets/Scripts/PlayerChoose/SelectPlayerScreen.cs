using System.Collections.Generic;
using UnityEngine;

namespace PlayerChoose
{
    public class SelectPlayerScreen : MonoBehaviour
    {
        public static Dictionary<Player.Player, int> SelectedCharacters = new Dictionary<Player.Player, int>();

        private void Start()
        {
            var animators = FindObjectsOfType<Animator>();
            foreach (var a in animators)
            {
                var animatorName = a.runtimeAnimatorController.name;
                var index = animatorName.Substring(animatorName.Length - 1, 1);
                var animName = "player_" + index + "_Walk";
                a.Play(animName);
            }
        }

        public static void SelectCharacter(Player.Player playerController, int currentPlayer)
        {
            SelectedCharacters.Add(playerController, currentPlayer);
        }
    }
}