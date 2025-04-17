using ScreenTest.General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenTest.UI
{
    [RequireComponent(typeof(UIAutoSizer))]
    public class ProductSlot : MonoBehaviour, IIniting
    {
        #region Actions
        public System.Action<ProductSlot> OnSlotClick;
        #endregion

        #region Serialize Fields
        [SerializeField] private Image m_Image;
        [SerializeField] private TMP_Text m_DescriptionText;
        #endregion

        #region Fields
        private ProductData m_Data;
        #endregion

        #region Properties
        public ProductData Data => m_Data;
        #endregion

        #region Methods
        public void Init()
        {
        }

        public void SetData(ProductData data)
        {
            m_Data = data;

            m_Image.sprite = data.Sprite;
            m_DescriptionText.text = data.Description;
        }

        public void ClickSlot()
        {
            OnSlotClick?.Invoke(this);
        }

        private void OnEnable()
        {
            GetComponent<UIAutoSizer>().InitSize();
        }
        #endregion
    }
}
