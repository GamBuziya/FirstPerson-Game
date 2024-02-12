using System;
namespace DefaultNamespace.Events
{
    public class CollectItemEvent
    {
        public event Action<string> onItemCollected;
        public void ItemCollected(string name)
        {
            if (onItemCollected != null)
            {
                onItemCollected(name);
            }
        }
    }
}