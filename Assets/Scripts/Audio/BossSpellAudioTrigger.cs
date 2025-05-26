using UnityEngine;

public class BossSpellAudioTrigger : MonoBehaviour
{
    [Header("BGM Settings")]
    public AudioSource spellbgmSource;
    public AudioClip spellbgmClip;

    //spell‚Ì‰¹
    private void SpellAudioSourceTrigger()
    {
        PlayBGM_Spell();
    }

    //spell
    public void PlayBGM_Spell()
    {

        if (spellbgmSource != null && spellbgmClip != null)
        {
            spellbgmSource.volume = 0.5f;
            spellbgmSource.clip = spellbgmClip;
            spellbgmSource.Play();
        }
    }
}
