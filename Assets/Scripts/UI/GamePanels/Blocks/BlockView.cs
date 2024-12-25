using Models.Datas;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePanels.BlockTowerPanels.Blocks
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetData(BlockModel model)
        {
            _image.color = model.Color;
        }


    }
}
