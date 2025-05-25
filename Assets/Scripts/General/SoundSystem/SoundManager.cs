using System.Collections.Generic;
using UnityEngine;

namespace ScreenTest.General.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour, IIniting
    {
        #region Serialize Fields
        [SerializeField] private SoundEffect m_SoundEffect;
        [SerializeField] private List<EffectPool> m_EffectPools;
        [SerializeField] private List<AudioClip> m_Music;
        [SerializeField] private int m_MaxEffectCount;
        #endregion

        #region Fields
        private List<GameObject> m_EffectObjects;
        private AudioSource m_AudioSource;
        #endregion

        public static SoundManager Instance { get; private set; }

        #region Methods
        public void Init()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
                
                m_AudioSource = GetComponent<AudioSource>();

                InitEffects();
                InitMusic();
                CheckMusic();
            }
            else Destroy(gameObject);
        }

        #region Music Methods
        private void InitMusic()
        {
            m_AudioSource.clip = m_Music[0];
            m_AudioSource.Play();

            m_Music.Add(m_Music[0]);
            m_Music.RemoveAt(0);
        }
        public void PlayMusic()
        {
            m_AudioSource.volume = 1;
        }
        public void StopMusic()
        {
            m_AudioSource.volume = 0;
        }
        public void CheckMusic()
        {
            if (PlayerPrefs.GetInt("Music") == 1)
                PlayMusic();
            else
                StopMusic();
        }
        #endregion

        #region Effect Methods
        private void InitEffects()
        {
            m_EffectObjects = new List<GameObject>();

            for (int i = 0; i < m_MaxEffectCount; i++)
            {
                m_EffectObjects.Add(Instantiate(m_SoundEffect, transform).gameObject);
                m_EffectObjects[i].SetActive(false);

                DontDestroyOnLoad(m_EffectObjects[i]);
            }
        }
        public AudioClip GetEffect(SoundType usingPool, string effectName)
        {
            EffectPool pool = m_EffectPools.Find(x => x.Type == usingPool);

            if (pool.Effects.Exists(x => x.Name == effectName))
            {
                return pool.Effects.Find(x => x.Name == effectName).Clip;
            }

            return null;
        }
        public void PlayEffect(SoundType usingPool, string effectName, Vector3 position, bool isUI = false)
        {
            EffectPool pool = m_EffectPools.Find(x => x.Type == usingPool);

            if (pool.Effects.Exists(x => x.Name == effectName))
            {
                if (!m_EffectObjects.Exists(x => x.activeInHierarchy == false))
                {
                    m_EffectObjects[0].SetActive(false);
                    m_EffectObjects.Add(m_EffectObjects[0]);
                    m_EffectObjects.RemoveAt(0);
                }
                GameObject newEffect = m_EffectObjects.Find(x => x.activeInHierarchy == false);

                newEffect.GetComponent<SoundEffect>().Init(pool.Effects.Find(x => x.Name == effectName).Clip, isUI);
                newEffect.transform.position = position;
            }
            else
            {
                Debug.LogError($"Not found sound \"{effectName}\" in pool \"{usingPool}\"");
            }
        }
        #endregion

        #region Unity Methods
        public void OnValidate()
        {
            foreach (var pool in m_EffectPools)
            {
                foreach (var effect in pool.Effects)
                {
                    if (effect.Clip != null)
                        effect.Name = effect.Clip.name;
                }
            }
        }
        private void Update()
        {
            if (!m_AudioSource.isPlaying)
            {
                InitMusic();
            }
        }
        #endregion

        #endregion
    }
}