using UnityEngine;

public class BossAudioManager : MonoBehaviour
{
    [Header("BGM Settings")]
    public AudioSource bossAudioSource; // �{�XBGM�Đ��p
    public AudioClip bossBgmClip;       // �{�X��BGM
    public AudioSource normalBgmSource;    // �ʏ�BGM�i�Q�[���J�n����BGM�j

    public bool hasTriggered = false; // ��x�����Đ����Ȃ��悤�ɂ���t���O

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return;

        if (collision.CompareTag("Player")) // Player�^�O�ƈ�v���邩�m�F
        {
            hasTriggered = true;

            // �ʏ�BGM���~
            if (normalBgmSource != null && normalBgmSource.isPlaying)
            {
                normalBgmSource.Stop();
            }

            if (bossAudioSource != null && bossBgmClip != null)
            {
                bossAudioSource.clip = bossBgmClip;
                bossAudioSource.loop = true;
                bossAudioSource.Play();
            }
        }
    }
}
