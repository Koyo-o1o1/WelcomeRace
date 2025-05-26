using UnityEngine;

public class CameraFollowX : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float smoothSpeed = 0.125f; // 追従の滑らかさ
    public float offsetX = 0f; // カメラとプレイヤーのX軸の距離

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = new Vector3(player.position.x + offsetX, currentPosition.y, currentPosition.z);
            transform.position = Vector3.Lerp(currentPosition, targetPosition, smoothSpeed);
        }
    }
}
