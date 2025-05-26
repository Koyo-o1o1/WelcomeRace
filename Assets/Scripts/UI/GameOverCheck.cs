using UnityEngine;

public class GameOverCheck : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Character_Stats character_Stats;

    void OnTriggerEnter2D(Collider2D other)
    {
        // "Player"�Ƃ����^�O���t�����I�u�W�F�N�g�ƏՓ˂����ꍇ
        if (other.CompareTag("Player"))
        {
            character_Stats.currentHealth = 0;

            // HP�o�[���f(?��null check)
            character_Stats.onHealthChanged?.Invoke();
            player.stateMachine.ChangeState(player.deadState);

            Time.timeScale = 0;
        }
    }
}
