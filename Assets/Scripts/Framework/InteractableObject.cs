using UnityEngine;
using UnityEngine.Events;

namespace Framework
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public UnityEvent onInteract = new();
    }
}