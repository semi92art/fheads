using UnityEngine;

namespace Common.Helpers
{
    public class AudioSourceHelper : MonoBehaviour
    {
        public AudioSource source;

        private void OnEnable()
        {
            source = GetComponent<AudioSource>();
        }
    }
}