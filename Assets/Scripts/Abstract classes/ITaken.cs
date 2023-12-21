using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public interface ITaken
    {
        bool IsEquipped { get; set; }
        void Take(GameObject gameObject);

    }
}