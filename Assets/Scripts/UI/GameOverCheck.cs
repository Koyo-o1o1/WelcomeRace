using UnityEngine;

public class GameOverCheck : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Character_Stats character_Stats;

    void OnTriggerEnter2D(Collider2D other)
    {
        // "Player"というタグが付いたオブジェクトと衝突した場合
        if (other.CompareTag("Player"))
        {
            character_Stats.currentHealth = 0;

            // HPバー反映(?はnull check)
            character_Stats.onHealthChanged?.Invoke();
            player.stateMachine.ChangeState(player.deadState);

            Time.timeScale = 0;
        }
    }
}
