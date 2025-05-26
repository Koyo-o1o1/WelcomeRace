using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PotionManager : MonoBehaviour
{

    [SerializeField] private ContinueUIManager ContinueUIManager;

    [Header("Potion Settings")]
    public GameObject potionPrefab; // **ポーションのプレハブ**
    public Transform potionContainer; // **ポーションを並べる親オブジェクト**
    [SerializeField] public int maxPotions; // **最大ポーション数（後から変更可能）**
    [SerializeField] public int healValue;
    public int currentPotions; // **現在のポーション数**
    private bool canUsePotion = true; // **クールダウン制御**
    public float potionCooldown = 2f; // **クールダウン時間**

    [Header("Player Stats")]
    public PlayerStats playerStats; // **プレイヤーのHP管理**

    [Header("BGM Settings")]
    public AudioSource potionBgmSource;
    public AudioClip potionBgmClip;

    private void Start()
    {
        currentPotions = maxPotions; // **初期値を最大に**
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
        //死亡時でないとき
        if (!ContinueUIManager.isContinue)
        {
            if (currentPotions > 0 && canUsePotion)
            {
                StartCoroutine(PotionCooldown());
                playerStats.Heal(healValue); // **HPを20回復（調整可能）**
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
            potionContainer.GetChild(i).gameObject.SetActive(true); // **ポーション数に応じて表示制御**
        }
    }

    private IEnumerator PotionCooldown()
    {
        canUsePotion = false;
        yield return new WaitForSeconds(potionCooldown);
        canUsePotion = true;
    }
}