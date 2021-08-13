using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LoadingMenu : MonoBehaviour
    {
        public Image imageLoading;
        public Text textLoading;

        private void Start() => StartCoroutine(CounterStart());

        private IEnumerator CounterStart()
        {
            float countSecond = 0;
            while (true)
            {
                countSecond += 0.01F;
                imageLoading.GetComponent<Image>().fillAmount = countSecond;
                textLoading.text = (Math.Round(countSecond, 2) * 100) + " %";
                if (countSecond >= 1)
                {
                    yield return new WaitForSeconds(0.2F);
                    SceneManager.LoadScene(1, LoadSceneMode.Single);
                    yield break;
                }
                yield return new WaitForSeconds(0.001F);
            }
        }
    }
}
