using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public ContinueUIManager continueUIManager;

    private Vector3 lastSavePoint;

    //最初の初期位置をセーブポイントへ
    private void Start()
    {
        // 初期位置をセーブポイントとして登録（プレイヤーの初期位置を取得）
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            UpdateSavePoint(player.transform.position);
        }
    }

    private void Awake()
    {
        // シングルトンパターン（1つだけ存在）
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも保持
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateSavePoint(Vector3 newPosition)
    {
        lastSavePoint = newPosition;
    }

    public Vector3 GetLastSavePoint()
    {
        return lastSavePoint;
    }
}
