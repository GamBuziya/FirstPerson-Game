using UnityEngine;

namespace DefaultNamespace.Enemy
{
    public class CanvasDisabler
    {
        private Canvas _canvas;

        public CanvasDisabler(Canvas canvas)
        {
            if(canvas == null) Debug.Log("_canvas == null");
            _canvas = canvas;
        }

        public void CanvasEnable() => _canvas.enabled = true;
        public void CanvasDisabled() => _canvas.enabled = false;
    }
}