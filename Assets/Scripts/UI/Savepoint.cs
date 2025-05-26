using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [Header("復活位置に使う子オブジェクト")]
    public Transform respawnPoint; // ← Emptyオブジェクトをインスペクターで割り当て

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.UpdateSavePoint(respawnPoint.position);
            //Debug.Log("セーブポイント更新: " + respawnPoint.position);
        }
    }
}
