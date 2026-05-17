using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyStruct
    {
        public GameObject enemy;
        public Vector2 spawnPosition;
    }
    [SerializeField]
    private EnemyStruct[] enemies;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (EnemyStruct item in enemies)
            {
                Instantiate(item.enemy, item.spawnPosition, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
