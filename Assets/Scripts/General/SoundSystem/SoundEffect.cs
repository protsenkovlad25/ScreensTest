using UnityEngine;

namespace ScreenTest.General.Sound
{
    [RequireComponent (typeof (AudioSource))]
    public class SoundEffect : MonoBehaviour
    {
        [SerializeField] bool m_IsInterface;
        
        private AudioSource m_AudioSource;
        private bool m_IsStopable;
        
        public bool IsPlaying => m_AudioSource.isPlaying;
        
        public void Init(AudioClip clip, bool isUI = false, bool isStopable = true)
        {
            gameObject.SetActive(true);
            
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioSource.clip = clip;
            m_AudioSource.volume = PlayerPrefs.GetInt("Sounds") == 0 ? 0 : 1f;
            m_AudioSource.Play();

            m_IsInterface = isUI;
            m_IsStopable = isStopable;
        }

        private void Update()
        {
            if (m_IsStopable)
            {
                if (!m_AudioSource.isPlaying)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}