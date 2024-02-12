using System;

namespace DefaultNamespace.Events
{
    public class InputEvents
    {
        public event Action onSubmitPressed;
        public void SubmitPressed()
        {
            if (onSubmitPressed != null)
            {
                onSubmitPressed();
            }
        }
    }
}