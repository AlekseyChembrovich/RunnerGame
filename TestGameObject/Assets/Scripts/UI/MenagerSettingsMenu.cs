using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MenagerSettingsMenu : MonoBehaviour
    {
        public Dropdown dropdownQuality;
        public Dropdown dropdownResolution;
        private Resolution[] arrayResolution;
        private GameObject gameObjectAudio;
        private bool activeMusic = true;
        public Button musicButton;
        public AudioClip audioClip;
        public Sprite spriteActive;
        public Sprite spriteStop;

        private void Start()
        {
            gameObjectAudio = GameObject.Find("AudioMenager");
            if (gameObjectAudio.GetComponent<AudioSource>().isPlaying)
            {
                musicButton.GetComponent<Image>().sprite = spriteActive;
                activeMusic = true;
            }
            else
            {
                musicButton.GetComponent<Image>().sprite = spriteStop;
                activeMusic = false;
            }

            dropdownQuality.ClearOptions();
            dropdownQuality.AddOptions(QualitySettings.names.ToList());
            dropdownQuality.value = QualitySettings.GetQualityLevel();

            arrayResolution = Screen.resolutions;
            var listStringResolution = new List<string>();
            for (var i = 0; i < arrayResolution.Count(); i++)
            {
                listStringResolution.Add(arrayResolution[i].ToString());
            }

            dropdownResolution.ClearOptions();
            dropdownResolution.AddOptions(listStringResolution);
            dropdownResolution.value = arrayResolution.Count() - 1;
            Screen.SetResolution(arrayResolution[arrayResolution.Count() - 1].width, arrayResolution[arrayResolution.Count() - 1].height, true);
        }

        public void ChangeQualityLevel() => QualitySettings.SetQualityLevel(dropdownQuality.value);

        public void ChangeResolution() => 
            Screen.SetResolution(arrayResolution[dropdownResolution.value].width, arrayResolution[dropdownResolution.value].height, true);

        public void BackButtonOnClick() => gameObject.SetActive(false);

        public void MusicButtonOnClick()
        {
            if (activeMusic)
            {
                musicButton.GetComponent<Image>().sprite = spriteStop;
                gameObjectAudio.GetComponent<AudioSource>().Stop();
                activeMusic = false;
            }
            else
            {
                musicButton.GetComponent<Image>().sprite = spriteActive;
                gameObjectAudio.GetComponent<AudioSource>().PlayOneShot(audioClip);
                activeMusic = true;
            }
        }
    }
}
