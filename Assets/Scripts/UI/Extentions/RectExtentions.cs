using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Extentions
{
    public static class RectExtentions
    {
        public static void SetAllZero(this RectTransform rt)
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }

        public static void SetAnchors(this RectTransform rt, Vector2 min, Vector2 max)
        {
            rt.anchorMin = min;
            rt.anchorMax = max;
        }

        public static void Refresh(this RectTransform item)
        {
            item.ForceUpdateRectTransforms();
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
        }

        public static void Refresh(this List<RectTransform> list)
        {
            foreach (var item in list)
            {
                item.ForceUpdateRectTransforms();
                LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            }
        }
    }
}
