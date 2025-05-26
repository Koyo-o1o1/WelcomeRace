using UnityEngine;

public class CrystalProjectile : MonoBehaviour
{
    [Header("crystal attack info")]
    [SerializeField] private float speed = 10f; // ���ˑ��x
    [SerializeField] private float damage = 10f; // �_���[�W��
    [SerializeField] private float lifetime = 3f; // ���݂��鎞��

    private Rigidbody2D rb;
    private Animator anim;
    private bool hasHit = false; // **�ŏ��� false �ɂ���**

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb.linearVelocity = transform.right * speed; // **�O���ɔ�΂�**
        anim.SetBool("Shoot", true); // **���˃A�j���[�V�����Đ�**

        Destroy(gameObject, lifetime); // **��莞�Ԍ�ɍ폜**
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // **�G�Ƀq�b�g�����ꍇ�̂ݍ폜**
        if (other.CompareTag("Enemy") && !hasHit)
        {
            hasHit = true;

            anim.SetBool("Shoot", false);
            anim.SetBool("Hit", true); // **�q�b�g�A�j���[�V�����Đ�**

            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage((int)damage); // **�_���[�W��K�p**
            }

            rb.linearVelocity *= 0.5f; // **���x������**
            rb.linearVelocity = Vector2.zero; // **���S��~**

            Destroy(gameObject, 0.5f); // **�A�j���[�V������ɍ폜**
        }
    }
}