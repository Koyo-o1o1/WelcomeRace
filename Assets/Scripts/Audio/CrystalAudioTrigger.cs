using UnityEngine;

public class CrystalAudioTrigger : MonoBehaviour
{
    [Header("BGM Settings")]
    public AudioSource crystalbgmSource;
    public AudioClip crystalbgmClip;

    //çUåÇÇÃâπ
    private void Start()
    {
        PlayBGM_Crystal();
    }

    //gameBGM
    public void PlayBGM_Crystal()
    {

        if (crystalbgmSource != null && crystalbgmClip != null)
        {
            crystalbgmSource.volume = 0.3f;
            crystalbgmSource.clip = crystalbgmClip;
            crystalbgmSource.Play();
        }
    }
}
