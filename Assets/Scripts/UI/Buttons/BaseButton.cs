using ScreenTest.General.Sound;
using UnityEngine;

namespace ScreenTest.UI
{
    public class BaseButton : MonoBehaviour
    {
        [Header("Click Sound")]
        [SerializeField] private SoundType m_SoundType;
        [SerializeField] private string m_SoundName;

        private void Start()
        {
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Click);
        }

        protected virtual void Click()
        {
            SoundManager.Instance.PlayEffect(m_SoundType, m_SoundName, transform.position);
        }
    }
}
