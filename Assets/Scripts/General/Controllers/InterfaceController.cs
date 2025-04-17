using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScreenTest.UI;

namespace ScreenTest.General
{
    public class InterfaceController : MonoBehaviour, IController
    {
        [SerializeField] private List<Panel> m_Panels;

        #region Methods
        public void Init()
        {
            foreach (var panel in m_Panels)
                panel.Init();
        }

        #region Open Methods
        public void OpenPanel(Type type, UnityAction onEndAction = null)
        {
            if (m_Panels.Exists(p => p.GetType() == type))
                OpenPanel(m_Panels.Find(p => p.GetType() == type), onEndAction);
            else
                throw new NotImplementedException($"Not found panel with type \"{type}\"");
        }
        public void OpenPanel(Panel panel, UnityAction onEndAction)
        {
            panel.Open(onEndAction);
        }
        public void OpenPanel(Panel panel)
        {
            panel.Open();
        }
        #endregion

        #region Close Methods
        public void ClosePanel(Type type, UnityAction onEndAction = null)
        {
            if (m_Panels.Exists(p => p.GetType() == type))
                ClosePanel(m_Panels.Find(p => p.GetType() == type), onEndAction);
            else
                throw new NotImplementedException($"Not found panel with type \"{type}\"");
        }
        public void ClosePanel(Panel panel, UnityAction onEndAction)
        {
            panel.Close(onEndAction);
        }
        public void ClosePanel(Panel panel)
        {
            panel.Close();
        }
        public void CloseAllOpenedPanels()
        {
            foreach (var panel in m_Panels)
            {
                if (panel.gameObject.activeSelf)
                {
                    ClosePanel(panel);
                }
            }
        }
        #endregion

        public void OpenInfoPanel(string text, UnityAction onEndAction = null)
        {
            InfoPanel panel = m_Panels.Find(p => p.GetType() == typeof(InfoPanel)) as InfoPanel;

            panel.SetText(text);

            OpenPanel(panel, onEndAction);
        }
        #endregion
    }
}
