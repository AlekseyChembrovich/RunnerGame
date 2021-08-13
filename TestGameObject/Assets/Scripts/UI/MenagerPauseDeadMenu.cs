using System.Collections;
using Assets.Scripts.PlayerSave;
using Assets.Scripts.Time;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MenagerPauseDeadMenu : MonoBehaviour
    {
        [Header("Панели управления")]
        public GameObject pauseMenu;
        public GameObject deadMenu;
        [Header("Texts отображающие информацию")]
        public Text newRecordTextBox;
        public Text countTextBox;
        public Text fpsTextBox;
        public Text coinsTextBox;
        [Header("Стартовый отсчёт")]
        public GameObject counterMenu;
        public Text counterText;

        private Transform transformCircle;
        private CounterSecondMenager counterSecondMenager;
        private bool isPauseMenuActive = false;
        private readonly MenagerSavePlayer menager = MenagerSavePlayer.GetMenager();
        private PlayerClass player;

        private void Start()
        {
            transformCircle = GameObject.FindGameObjectWithTag("Player").transform;

            pauseMenu.SetActive(false);
            deadMenu.SetActive(false);
            counterSecondMenager = countTextBox.GetComponent<CounterSecondMenager>();
            player = menager.ReadDataPlayer();

            StartCoroutine(CounterStart());
            StartCoroutine(CoroutineFPS());
        }
    
        private IEnumerator CounterStart()
        {
            float countSecond = 0;
            UnityEngine.Time.timeScale = 0.000001F;
            counterMenu.SetActive(true);
            while (true)
            {
                countSecond += 1;
                counterText.text = countSecond.ToString();
                if (countSecond > 3)
                {
                    counterMenu.SetActive(false);
                    UnityEngine.Time.timeScale = 1F;
                    yield break;
                }
                yield return new WaitForSeconds(0.000001F);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !deadMenu.activeSelf)
            {
                isPauseMenuActive = !isPauseMenuActive;
                ExecutePauseMenu(isPauseMenuActive);
            }

            if (!(transformCircle.localPosition.y <= -0.5F)) return;
            deadMenu.SetActive(true);

            if (menager.WriteDataPlayer(float.Parse(countTextBox.text), int.Parse(coinsTextBox.text)))
            {
                newRecordTextBox.enabled = true;
                newRecordTextBox.text = $"New record: {countTextBox.text}";
            }
            else if (int.Parse(coinsTextBox.text) > 0)
            {
                menager.WriteDataPlayer(int.Parse(coinsTextBox.text));
                coinsTextBox.text = "0";
            }

            UnityEngine.Time.timeScale = 0F;
            counterSecondMenager.isCount = false;
        }

        private IEnumerator CoroutineFPS()
        {
            while (true)
            {
                var fps = 1.0F / UnityEngine.Time.deltaTime;
                fpsTextBox.text = "FPS: " + (int)fps;
                yield return new WaitForSeconds(1F);
            }
        }

        private void ExecutePauseMenu(bool isActive)
        {
            counterSecondMenager.isCount = !counterSecondMenager.isCount; // Остановка или включение счетчика
            pauseMenu.SetActive(isActive);
            UnityEngine.Time.timeScale = isActive ? 0F : 1F;
        }

        public void ContinueButtonOnClick() => ExecutePauseMenu(false);

        public void MenuButtonOnClick()
        {
            newRecordTextBox.enabled = false;
            countTextBox.text = "0";
            counterSecondMenager.countSecond = 0;
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        public void RestartButtonOnClick()
        {
            UnityEngine.Time.timeScale = 1F;
            newRecordTextBox.enabled = false;
            countTextBox.text = "0";
            counterSecondMenager.countSecond = 0;
            SceneManager.LoadScene(2);
        }

        public void PauseButtonOnClick()
        {
            if (deadMenu.activeSelf) return;
            isPauseMenuActive = !isPauseMenuActive;
            ExecutePauseMenu(isPauseMenuActive);
        }
    }
}
