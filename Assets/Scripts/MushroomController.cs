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
    [SerializeField]
    private LayerMask ground;



    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0);
        if (speed > 0)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(REdge.position, rad, ground);

            foreach (var hit in hits)
            {
                if (hit.gameObject != gameObject)
                {
                    speed *= -1;
                }
            }


        }
        else
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(LEdge.position, rad, ground);

            foreach (var hit in hits)
            {
                if (hit.gameObject != gameObject)
                {
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
