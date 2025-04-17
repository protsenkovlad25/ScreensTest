using System.Collections.Generic;
using UnityEngine;

namespace ScreenTest.General.Configs
{
    [CreateAssetMenu(fileName = "ProductsConfig", menuName = "Scriptable Objects/ProductsConfig")]
    public class ProductsConfig : ScriptableObject
    {
        [SerializeField] private List<ProductData> m_ProductDatas;

        public List<ProductData> ProductDatas => m_ProductDatas;

        public ProductData GetData(string name)
        {
            if (m_ProductDatas.Exists(p => p.Name == name))
            {
                return m_ProductDatas.Find(p => p.Name == name);
            }
            else throw new System.NotImplementedException($"Not found product data with name \"{name}\"");
        }
        public ProductData GetData(int id)
        {
            if (m_ProductDatas.Exists(p => p.Id == id))
            {
                return m_ProductDatas.Find(p => p.Id == id);
            }
            else throw new System.NotImplementedException($"Not found product data with id \"{id}\"");
        }
    }
}
