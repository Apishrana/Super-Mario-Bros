using UnityEngine;

public class MushroomController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform REdge;
    [SerializeField]
    private Transform LEdge;
    [SerializeField]
    private float rad;


    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0);
        if (speed > 0)
        {
            Debug.Log(Physics2D.OverlapCircleAll(REdge.position, rad));
            Debug.Log(1);
            Collider2D[] hits = Physics2D.OverlapCircleAll(REdge.position, rad);

            foreach (var hit in hits)
            {
                if (hit.gameObject != gameObject)
                {
                    Debug.Log("Hit: " + hit.name);
                    speed *= -1;
                }
            }


        }
        else
        {
            Debug.Log(Physics2D.OverlapCircleAll(LEdge.position, rad));
            Debug.Log(2);
            Collider2D[] hits = Physics2D.OverlapCircleAll(LEdge.position, rad);

            foreach (var hit in hits)
            {
                if (hit.gameObject != gameObject)
                {
                    Debug.Log("Hit: " + hit.name);
                    speed *= -1;
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.pink;
        Gizmos.DrawWireSphere(REdge.position, rad);
        Gizmos.DrawWireSphere(LEdge.position, rad);
    }
}
