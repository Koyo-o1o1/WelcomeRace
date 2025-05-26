using UnityEngine;

public class BossAttackAudioTrigger : MonoBehaviour
{
    [Header("BGM Settings")]
    public AudioSource attackbgmSource;
    public AudioClip attackbgmClip;

    //çUåÇÇÃâπ
    private void AttackAudioSourceTrigger()
    {
        PlayBGM_Attack();
    }

    

    //attack
    public void PlayBGM_Attack()
    {

        if (attackbgmSource != null && attackbgmClip != null)
        {

            attackbgmSource.volume = 0.5f;
            attackbgmSource.clip = attackbgmClip;
            attackbgmSource.Play();
        }
    }

    
}
