using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class MenagerAudio : MonoBehaviour
    {
        private void Awake()
        {
            var obj = GameObject.FindWithTag("SourseMusic");
            if (obj != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                this.gameObject.tag = "SourseMusic";
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
