using UnityEngine;

namespace ScreenTest.General
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MainManager m_MainManager;

        private void Awake()
        {
            Application.targetFrameRate = 120;

            m_MainManager.Init();
        }
    }
}
