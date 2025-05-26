using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public ContinueUIManager continueUIManager;

    private Vector3 lastSavePoint;

    //�ŏ��̏����ʒu���Z�[�u�|�C���g��
    private void Start()
    {
        // �����ʒu���Z�[�u�|�C���g�Ƃ��ēo�^�i�v���C���[�̏����ʒu���擾�j
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            UpdateSavePoint(player.transform.position);
        }
    }

    private void Awake()
    {
        // �V���O���g���p�^�[���i1�������݁j
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ł��ێ�
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
