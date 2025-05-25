using UnityEngine;

namespace ScreenTest.General
{
    [System.Serializable]
    public class ProductData
    {
        #region Serialize Fields
        [SerializeField] private string m_Name;
        [SerializeField] private int m_Id;
        [SerializeField] private string m_Description;
        [SerializeField] private Sprite m_Sprite;
        #endregion

        #region Properties
        public int Id => m_Id;
        public string Name => m_Name;
        public string Description => m_Description;
        public Sprite Sprite => m_Sprite;
        #endregion
    }
}
