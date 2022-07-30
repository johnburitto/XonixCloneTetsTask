using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int NumberOfGroundEnemies { get; private set; }

    public void SpawnEnemies(LevelConfig levelConfig)
    {
        GameObject created;
        int positionX = 0;
        int poSitionY = 0;

        for (int i = 0; i < levelConfig.NumberOfEnemies; i++)
        {
            if (i % 2 == 0) 
            {
                created = Instantiate(levelConfig.GroundEnemyTemplate, transform);
                GenerateGroundEnemyPosition(out positionX, out poSitionY);

                created.transform.position = new Vector2(positionX, poSitionY);
                NumberOfGroundEnemies++;
            }

            created = Instantiate(levelConfig.SeaEnemyTamplate, transform);
            positionX = Random.Range(2, GameField.Instance.Width - 2);
            poSitionY = Random.Range(2, GameField.Instance.Height - 2);

            created.transform.position = new Vector2(positionX, poSitionY);
        }
    }

    private void GenerateGroundEnemyPosition(out int positionX, out int positionY)
    {
        if (Random.Range(0, 1) ==  0 ? true : false)
        {
            int leftOrRight = Random.Range(0, GameField.Instance.Width - 1);

            if (leftOrRight > GameField.Instance.Width / 2)
            {
                positionX = Random.Range(GameField.Instance.Width - 2, GameField.Instance.Width - 1);
            }
            else
            {
                positionX = Random.Range(0, 1);
            }

            positionY = Random.Range(0, GameField.Instance.Height - 1);
        }
        else
        {
            int upOrDown = Random.Range(0, GameField.Instance.Width - 1);

            if (upOrDown > GameField.Instance.Height / 2)
            {
                positionX = Random.Range(GameField.Instance.Height - 2, GameField.Instance.Height - 1);
            }
            else
            {
                positionX = Random.Range(0, 1);
            }

            positionY = Random.Range(0, GameField.Instance.Width - 1);
        }
    }

    public void ResetEnemies(LevelConfig levelConfig)
    {
        foreach (Transform element in transform)
        {
            Destroy(element.gameObject);
        }

        NumberOfGroundEnemies = 0;
        SpawnEnemies(levelConfig);
    }
}
