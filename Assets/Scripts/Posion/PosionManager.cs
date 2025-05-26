using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PotionManager : MonoBehaviour
{

    [SerializeField] private ContinueUIManager ContinueUIManager;

    [Header("Potion Settings")]
    public GameObject potionPrefab; // **�|�[�V�����̃v���n�u**
    public Transform potionContainer; // **�|�[�V��������ׂ�e�I�u�W�F�N�g**
    [SerializeField] public int maxPotions; // **�ő�|�[�V�������i�ォ��ύX�\�j**
    [SerializeField] public int healValue;
    public int currentPotions; // **���݂̃|�[�V������**
    private bool canUsePotion = true; // **�N�[���_�E������**
    public float potionCooldown = 2f; // **�N�[���_�E������**

    [Header("Player Stats")]
    public PlayerStats playerStats; // **�v���C���[��HP�Ǘ�**

    [Header("BGM Settings")]
    public AudioSource potionBgmSource;
    public AudioClip potionBgmClip;

    private void Start()
    {
        currentPotions = maxPotions; // **�����l���ő��**
        SpawnPotions();
    }

    public void SpawnPotions()
    {
        

        for (int i = 0; i < maxPotions; i++)
        {
            GameObject potion = Instantiate(potionPrefab, potionContainer);
        }
    }

    public void UsePotion()
    {
        //���S���łȂ��Ƃ�
        if (!ContinueUIManager.isContinue)
        {
            if (currentPotions > 0 && canUsePotion)
            {
                StartCoroutine(PotionCooldown());
                playerStats.Heal(healValue); // **HP��20�񕜁i�����\�j**
                currentPotions--;

                UpdatePotionUI();

                if (potionBgmSource != null && potionBgmClip != null)
                {

                    potionBgmSource.volume = 0.4f;
                    potionBgmSource.clip = potionBgmClip;
                    potionBgmSource.Play();
                }
            }
        }
    }

    public void UpdatePotionUI()
    {
        foreach (Transform potion in potionContainer)
        {
            potion.gameObject.SetActive(false);
        }

        for (int i = 0; i < currentPotions; i++)
        {
            potionContainer.GetChild(i).gameObject.SetActive(true); // **�|�[�V�������ɉ����ĕ\������**
        }
    }

    private IEnumerator PotionCooldown()
    {
        canUsePotion = false;
        yield return new WaitForSeconds(potionCooldown);
        canUsePotion = true;
    }
}