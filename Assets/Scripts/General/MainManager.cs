using ScreenTest.UI;
using UnityEngine;
using ScreenTest.General.Sound;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScreenTest.General
{
    public class MainManager : MonoBehaviour, IIniting
    {
        #region Serialize Fields
        [SerializeField] private SoundManager m_SoundManager;
        [SerializeField] private MenuController m_MenuController;
        [SerializeField] private ShopController m_ShopController;
        [SerializeField] private SettingsController m_SettingsController;
        [SerializeField] private InterfaceController m_InterfaceController;
        #endregion

        #region Methods
        public void Init()
        {
            m_MenuController.Init();
            m_ShopController.Init();
            m_SettingsController.Init();
            m_InterfaceController.Init();
            
            m_SoundManager.Init();

            StartApp();
        }

        public void StartApp()
        {
            m_InterfaceController.OpenPanel(typeof(MenuPanel));
        }

        public void QuitApp()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        #endregion
    }
}
