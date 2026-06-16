using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private EnemyHealth[] enemies;

    private bool victoryShown;

    private void Start()
    {
        enemies = Object.FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);
    }

    private void Update()
    {
        if (victoryShown)
            return;

        int aliveEnemies = 0;

        foreach (EnemyHealth enemy
            in enemies)
        {
            if (enemy != null &&
                !enemy.IsDead)
            {
                aliveEnemies++;
            }
        }

        if (aliveEnemies == 0)
        {
            victoryShown = true;

            VictoryManager
                .Instance
                .ShowVictory();
        }
    }
}