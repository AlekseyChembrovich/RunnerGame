using Assets.Scripts.PlayerSave;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MenagerStartMenu : MonoBehaviour
    {
        public Text recordText;
        public Text coinsText;
        private MenagerSavePlayer menager = MenagerSavePlayer.GetMenager();
        private PlayerClass player;

        private void Start()
        {
            player = menager.ReadDataPlayer();
            recordText.text = $"Your record: {player.CountSecond}";
            coinsText.text = $"Coins count: {player.CountCoins}";
        }

        public void PlayButtonOnClick() => SceneManager.LoadScene(2, LoadSceneMode.Single);

        public void SettingsButtonOnClick(GameObject gameObject) => gameObject.SetActive(true);

        public void ExitButtonOnClick() => Application.Quit();
    }
}
