using UnityEngine;

public class CrystalProjectile : MonoBehaviour
{
    [Header("crystal attack info")]
    [SerializeField] private float speed = 10f; // 発射速度
    [SerializeField] private float damage = 10f; // ダメージ量
    [SerializeField] private float lifetime = 3f; // 存在する時間

    private Rigidbody2D rb;
    private Animator anim;
    private bool hasHit = false; // **最初は false にする**

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb.linearVelocity = transform.right * speed; // **前方に飛ばす**
        anim.SetBool("Shoot", true); // **発射アニメーション再生**

        Destroy(gameObject, lifetime); // **一定時間後に削除**
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // **敵にヒットした場合のみ削除**
        if (other.CompareTag("Enemy") && !hasHit)
        {
            hasHit = true;

            anim.SetBool("Shoot", false);
            anim.SetBool("Hit", true); // **ヒットアニメーション再生**

            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage((int)damage); // **ダメージを適用**
            }

            rb.linearVelocity *= 0.5f; // **速度を減速**
            rb.linearVelocity = Vector2.zero; // **完全停止**

            Destroy(gameObject, 0.5f); // **アニメーション後に削除**
        }
    }
}