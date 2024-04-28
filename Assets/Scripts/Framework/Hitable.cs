using UnityEngine;

namespace Framework
{
    [RequireComponent(typeof(HealthData), typeof(TagManager))]
    public sealed class Hitable : MonoBehaviour
    {
        private TagManager _tagManager;
        private HealthData _healthData;

        public TagManager GetTagManager() => _tagManager;
        
        private void Awake()
        {
            _tagManager = GetComponent<TagManager>();
            _healthData = GetComponent<HealthData>();
        }
    }
}