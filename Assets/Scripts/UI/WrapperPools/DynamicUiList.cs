using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MetaGame.Inventory.WrapperPools
{
    public class DynamicUiList<T>
           where T : Component
    {
        private WrapperPool<T> _pool;
        private List<T> _views = new();
        private ReactiveCommand<T> _onSelect = new();
        private Transform _parent;

        public IObservable<T> OnSelect => _onSelect;
        public List<T> Views => _views;

        public DynamicUiList(T prefab, Action<T> onCreateAction, Transform content)
        {
            _parent = content;
            _pool = new WrapperPool<T>(prefab, onCreateAction, content);
        }

        public void GetNewViews(int count, out List<T> views)
        {
            ClearList();
            for (var i = 0; i < count; i++)
            {
                var view = _pool.Get();
                _views.Add(view);
            }
            views = _views;
        }

        public void GetViews(int count, out List<T> views)
        {
            for (var i = 0; i < count; i++)
            {
                var view = _pool.Get();
                _views.Add(view);
            }
            views = _views;
        }

        public void ClearList()
        {
            for (var index = _views.Count - 1; index >= 0; index--)
            {
                _views[index].transform.SetParent(_parent);
                _pool.Release(_views[index]);
            }

            _views.Clear();
        }
    }
}
