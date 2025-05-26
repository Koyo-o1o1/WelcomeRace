using UnityEngine;
using UnityEngine.UI;

public class PotionItem : MonoBehaviour
{
    [SerializeField] private PotionManager potionManager;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => potionManager.UsePotion());
    }
}