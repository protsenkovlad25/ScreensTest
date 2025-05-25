using UnityEngine;

namespace ScreenTest.General.Sound
{
    [System.Serializable]
    public class SoundEffectData
    {
        [SerializeField] private string m_Name;
        [SerializeField] private AudioClip m_Clip;

        public string Name { get => m_Name; set => m_Name = value; }
        public AudioClip Clip => m_Clip;
    }
}
