using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

namespace PlayerChoose
{
    public class CharacterArrow : MonoBehaviour
    {
        public Player.Player PlayerController;
        public bool IsSelected;
        public int CurrentCharacter;
        private Vector2 _originalPosition;
        private GameObject[] _characters;

        private void Start()
        {
            _originalPosition = GetComponent<RectTransform>().anchoredPosition;
            _characters = FindObjectOfType<CharacterPool>().CharacterInstances;
            CurrentCharacter = Game.PlayerCharacters[PlayerController];
            ChangePosition(CurrentCharacter);
        }

        public void SelectPlayer()
        {
            var zeroIndexCharacter = CurrentCharacter - 1;
            var selectedCharacter = _characters[zeroIndexCharacter];
            selectedCharacter.GetComponent<Character>().IsSelected = true;
            selectedCharacter.GetComponentInChildren<Animator>().enabled = false;
            selectedCharacter.GetComponentInChildren<Image>().color = new Color(0.25f, 0.25f, 0.25f);
            IsSelected = true;
            FindObjectOfType<SelectScreen>().PlayerSelected();
            StartCoroutine(AnimateArrow());
            Game.SetCharacter(PlayerController, zeroIndexCharacter + 1);
        }

        private IEnumerator AnimateArrow()
        {
            var rectTransform = GetComponent<RectTransform>();
            while (true)
            {
                rectTransform.anchoredPosition = new Vector2(
                    _originalPosition.x,
                    _originalPosition.y + Mathf.Sin(Time.time * 20) * 10
                );
                yield return null;
            }
        }

        private void Update () {
            if (IsSelected) return;

            if (PlayerInput.Direction(MoveDirection.Left, PlayerController))
            {
                CurrentCharacter = Mathf.Clamp(CurrentCharacter - 1, 1, _characters.Length);
                ChangePosition(CurrentCharacter);
            }
            else if (PlayerInput.Direction(MoveDirection.Right, PlayerController))
            {
                CurrentCharacter = Mathf.Clamp(CurrentCharacter + 1, 1, _characters.Length);
                ChangePosition(CurrentCharacter);
            }
            else if (PlayerInput.Direction(MoveDirection.Down, PlayerController))
            {
                CurrentCharacter = Mathf.Clamp(CurrentCharacter + 4, 1, _characters.Length);
                ChangePosition(CurrentCharacter);
            }
            else if (PlayerInput.Direction(MoveDirection.Up, PlayerController))
            {
                CurrentCharacter = Mathf.Clamp(CurrentCharacter - 4, 1, _characters.Length);
                ChangePosition(CurrentCharacter);
            }

            if (PlayerInput.GetButtonDown("A", PlayerController))
            {
                if (!_characters[CurrentCharacter-1].GetComponent<Character>().IsSelected)
                {
                    SelectPlayer();
                }
            }
        }

        private void ChangePosition(int currentPlayer)
        {
            transform.SetParent(_characters[currentPlayer - 1].transform);
            GetComponent<RectTransform>().anchoredPosition = _originalPosition;
        }
    }
}
