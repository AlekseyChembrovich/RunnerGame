using Assets.Scripts.PlayerSave;
using Assets.Scripts.Time;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Trigger
{
    public class TriggerController : MonoBehaviour
    {
        private GameObject deadMenu;

        private Text[] TextBoxs;
        private Text newRecordTextBox;
        private Text countTextBox;
        private Text coinsTextBox;

        private CounterSecondMenager counterSecondMenager;
        private MenagerSavePlayer menager = MenagerSavePlayer.GetMenager();
        private PlayerClass player;

        private void Start()
        {
            var gameObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.name == "PanelDead")
                {
                    deadMenu = gameObject;
                }
            }

            TextBoxs = Resources.FindObjectsOfTypeAll(typeof(Text)) as Text[];
            foreach (var textBox in TextBoxs)
            {
                switch (textBox.gameObject.name)
                {
                    case "TextCount":
                        countTextBox = textBox;
                        break;
                    case "TextRecord":
                        newRecordTextBox = textBox;
                        break;
                    case "TextCoins":
                        coinsTextBox = textBox;
                        break;
                }
            }
            counterSecondMenager = countTextBox.GetComponent<CounterSecondMenager>();
            player = menager.ReadDataPlayer();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Player") return;
            deadMenu.SetActive(true);

            if (menager.WriteDataPlayer(float.Parse(countTextBox.text), int.Parse(coinsTextBox.text)))
            {
                newRecordTextBox.enabled = true;
                newRecordTextBox.text = $"New record: {coinsTextBox.text}";
            }
            else
            {
                menager.WriteDataPlayer(int.Parse(coinsTextBox.text));
                coinsTextBox.text = "0";
            }

            UnityEngine.Time.timeScale = 0F;
            counterSecondMenager.isCount = false;
        }
    }
}
