using System.Collections.Generic;
using UnityEngine;

namespace ScreenTest.General.Sound
{
    [System.Serializable]
    public struct EffectPool
    {
        [SerializeField] private SoundType m_Type;
        [SerializeField] private List<SoundEffectData> m_Effects;

        public SoundType Type => m_Type;
        public List<SoundEffectData> Effects => m_Effects;
    }
}
