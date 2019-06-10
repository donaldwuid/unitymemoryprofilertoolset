using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Editor.Treemap
{
    interface ITreemapRenderable
    {
        Color GetColor();
        Rect GetPosition();
        string GetLabel();
    }
}
