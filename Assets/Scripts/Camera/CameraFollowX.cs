using UnityEngine;

public class CameraFollowX : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public float smoothSpeed = 0.125f; // �Ǐ]�̊��炩��
    public float offsetX = 0f; // �J�����ƃv���C���[��X���̋���

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
