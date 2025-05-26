using UnityEngine;

public class BossAudioManager : MonoBehaviour
{
    [Header("BGM Settings")]
    public AudioSource bossAudioSource; // ボスBGM再生用
    public AudioClip bossBgmClip;       // ボスのBGM
    public AudioSource normalBgmSource;    // 通常BGM（ゲーム開始時のBGM）

    public bool hasTriggered = false; // 一度しか再生しないようにするフラグ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return;

        if (collision.CompareTag("Player")) // Playerタグと一致するか確認
        {
            hasTriggered = true;

            // 通常BGMを停止
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
