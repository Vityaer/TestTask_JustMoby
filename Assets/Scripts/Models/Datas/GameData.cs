using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models.Datas
{
    public class GameData
    {
        [SerializeField] private int _blockStorageCount;
        [SerializeField] private List<Color> _colors = new();

        public int BlockStorageCount => _blockStorageCount;
        public IReadOnlyList<Color> Colors => _colors;
    }
}
