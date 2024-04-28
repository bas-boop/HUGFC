using UnityEngine;

namespace NPC.Turret
{
    // [RequireComponent(typeof(Timer))]
    public sealed class Lamp : MonoBehaviour
    {
        [SerializeField] private Material[] lamps;
        [SerializeField] private MeshRenderer renderer;
        
        private bool _isToggled;

        private void Start()
        {
            Invoke(nameof(ToggleLamp), 1);
        }

        public void ToggleLamp()
        {
            renderer.material = _isToggled ? lamps[1] : lamps[0];
            _isToggled = !_isToggled;
            
            Invoke(nameof(ToggleLamp), 1);
        }
    }
}