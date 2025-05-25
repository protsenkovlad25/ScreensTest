using ScreenTest.General.Sound;
using ScreenTest.UI;
using UnityEngine;

namespace ScreenTest.General
{
    public class SettingsController : MonoBehaviour, IController
    {
        [SerializeField] private SettingsPanel m_SettingsPanel;

        #region Methods
        public void Init()
        {
            m_SettingsPanel.OnSettingChanged = ChangeSetting;

            CheckSettings();
        }

        private void CheckSettings()
        {
            if (!PlayerPrefs.HasKey("Music"))
                PlayerPrefs.SetInt("Music", 1);

            if (!PlayerPrefs.HasKey("Sounds"))
                PlayerPrefs.SetInt("Sounds", 1);
        }

        private void ChangeSetting(string setting, bool value)
        {
            PlayerPrefs.SetInt(setting, System.Convert.ToInt16(value));

            SoundManager.Instance?.CheckMusic();
        }
        private void ChangeSetting(string setting, float value)
        {
            PlayerPrefs.SetFloat(setting, value);
        }
        private void ChangeSetting(string setting, string value)
        {
            PlayerPrefs.SetString(setting, value);
        }
        #endregion
    }
}
