using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerChoose
{
    public class CharacterPool : MonoBehaviour
    {
        [Description("Characters per row")] public int Columns = 4;
        public GameObject[] Characters;

        private void Start()
        {
            for (var i = 0; i < Characters.Length; i++)
            {
                InstantiateCharacter(Characters[i], i);
            }
        }

        private void InstantiateCharacter(GameObject go, int position)
        {
            var character = Instantiate(go, transform);

            var rectangle = FindObjectOfType<CanvasScaler>().referenceResolution;
            var rows = Mathf.Max((Characters.Length - 1) / Columns + 1, 1);

            var rowHeight = rectangle.y / rows;
            var columnWidth = rectangle.x / Columns;

            var row = position / Columns + 1;
            var column = position % Columns + 1;

            var characterSize = go.GetComponent<RectTransform>().sizeDelta;

            var x = (column * columnWidth) - (columnWidth / 2);
            var y = -(row * rowHeight) + (rowHeight / 2) + characterSize.y / 2;

            character.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
            character.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
    }
}