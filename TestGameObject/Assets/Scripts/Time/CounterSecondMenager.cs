using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Time
{
    public class CounterSecondMenager : MonoBehaviour
    {
        public float countSecond = 0;
        public bool isCount = true;

        private Text textBox;

        private void Start()
        {
            textBox = gameObject.GetComponent<Text>();
            isCount = true;
            textBox.text = "0";
        }

        private void Update()
        {
            if (!isCount) return;
            countSecond += UnityEngine.Time.deltaTime;
            textBox.text = Mathf.Round(countSecond).ToString();
        }
    }
}
