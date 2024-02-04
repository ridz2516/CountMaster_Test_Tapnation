
using UnityEngine;

public class EndPlatform : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _Confetti;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(nameof(eTags.Player)))
        {
            for (int i = 0; i < _Confetti.Length; i++)
            {
                _Confetti[i].Play();
            }
        }
    }

}
