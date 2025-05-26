using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [Header("�����ʒu�Ɏg���q�I�u�W�F�N�g")]
    public Transform respawnPoint; // �� Empty�I�u�W�F�N�g���C���X�y�N�^�[�Ŋ��蓖��

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.UpdateSavePoint(respawnPoint.position);
            //Debug.Log("�Z�[�u�|�C���g�X�V: " + respawnPoint.position);
        }
    }
}
