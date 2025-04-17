using ScreenTest.General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSettingSlot : MonoBehaviour, IIniting
{
    #region Actions
    public System.Action<string, bool> OnToggleChanged;
    #endregion

    #region Serialize Fields
    [Header("Objects")]
    [SerializeField] private TMP_Text m_Text;
    [SerializeField] private TMP_Text m_ToggleText;
    [SerializeField] private Toggle m_Toggle;

    [Header("Setting Name")]
    [SerializeField] private string m_SettingName;
    #endregion

    #region Methods
    public void Init()
    {
        m_Toggle.isOn = System.Convert.ToBoolean(PlayerPrefs.GetInt(m_SettingName));
    }

    public void ToggleChanged()
    {
        OnToggleChanged?.Invoke(m_SettingName, m_Toggle.isOn);

        m_ToggleText.text = m_Toggle.isOn ? "Вкл." : "Выкл.";

        Debug.Log($"Changed \"{m_SettingName}\" - {m_Toggle.isOn}");
    }
    #endregion
}
