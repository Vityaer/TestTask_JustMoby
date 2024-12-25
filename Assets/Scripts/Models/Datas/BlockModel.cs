using System;
using UnityEngine;

namespace Models.Datas
{
    [Serializable]
    public class BlockModel
    {
        private readonly Color _color;

        public Color Color => _color;

        public BlockModel(Color color)
        {
            _color = color;
        }
    }
}
