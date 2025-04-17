using UnityEngine;

namespace ScreenTest.General
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MainManager m_MainManager;

        private void Awake()
        {
            m_MainManager.Init();
        }
    }
}
