using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerChoose
{
    public class SelectPlayerArrow : MonoBehaviour
    {
        public GameObject[] Characters;
        public Player PlayerController;
        public int CurrentCharacter = 1;
        public bool IsSelected;
        private float _originalY;

        private void Start()
        {
            _originalY = transform.localPosition.y;
        }

        public void SelectPlayer(int index)
        {
            SelectPlayerScreen.SelectCharacter(PlayerController, CurrentCharacter);
            SelectPlayerScreen.SelectedCharacters[PlayerController] = index;
            Characters[index].GetComponent<Animator>().Stop();
            Characters[index].GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.25f, 0.25f);
            IsSelected = true;
            StartCoroutine(AnimateArrow());
        }

        private IEnumerator AnimateArrow()
        {
            while (true)
            {
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    _originalY + Mathf.Sin(Time.time * 20) / 20,
                    transform.localPosition.z
                );
                yield return null;
            }
        }

        private void Update () {
            if (IsSelected) return;

            if (PlayerInput.Direction(MoveDirection.Left, PlayerController))
            {
                CurrentCharacter = Mathf.Clamp(CurrentCharacter - 1, 1, Characters.Length);
                ChangePosition(CurrentCharacter);
            }
            else if (PlayerInput.Direction(MoveDirection.Right, PlayerController))
            {
                CurrentCharacter = Mathf.Clamp(CurrentCharacter + 1, 1, Characters.Length);
                ChangePosition(CurrentCharacter);
            }

            if (PlayerInput.GetButtonDown("A", PlayerController))
            {
                if (!SelectPlayerScreen.SelectedCharacters.ContainsValue(CurrentCharacter))
                {
                    Debug.Log("Selected " + CurrentCharacter);
                    SelectPlayer(CurrentCharacter - 1);
                }
            }
        }

        private void ChangePosition(int currentPlayer)
        {
            var localPos = transform.localPosition;
            transform.parent = Characters[currentPlayer - 1].transform;
            transform.localPosition = localPos;
        }
    }
}
