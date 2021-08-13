using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Spawn
{
    public class Coin : MonoBehaviour
    {
        private Text[] TextBoxs;
        private Text coinsTextBox;

        private void Start()
        {
            TextBoxs = Resources.FindObjectsOfTypeAll(typeof(Text)) as Text[];

            foreach (var textBox in TextBoxs)
            {
                if (textBox.gameObject.name == "TextCoins")
                {
                    coinsTextBox = textBox;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Player") return;
            coinsTextBox.text = (int.Parse(coinsTextBox.text) + 1).ToString();
            Destroy(this.gameObject);
        }
    }
}
