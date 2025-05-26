using UnityEngine;
using UnityEngine.Audio;

public class PlayerAttackAudioTrigger : MonoBehaviour
{
    [Header("BGM Settings")]
    public AudioSource attackbgmSource;
    public AudioClip attackbgmClip;

    //çUåÇÇÃâπ
    private void AttackAudioSourceTrigger()
    {
        PlayBGM_Attack();
    }

    //gameBGM
    public void PlayBGM_Attack()
    {

        if (attackbgmSource != null && attackbgmClip != null)
        {

            attackbgmSource.volume = 0.3f;
            attackbgmSource.clip = attackbgmClip;
            attackbgmSource.Play();
        }
    }
}
