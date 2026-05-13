using System.Collections;
using UnityEngine;

public class GoombaController : MonoBehaviour
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
    [SerializeField]
    private Sprite deathSprite;



    void Start()
    {
        speed *= -1;
    }


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
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bounds"))
        {
            transform.GetComponent<BoxCollider2D>().enabled = false;
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;
            StartCoroutine(DestroySelf(2f));
        }
    }

    IEnumerator DestroySelf(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public void Kill()
    {
        speed = 0;
        transform.GetComponent<BoxCollider2D>().enabled = false;
        transform.GetComponent<Animation>().enabled = false;
        transform.GetComponent<Animator>().enabled = false;
        transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        transform.GetComponent<SpriteRenderer>().sprite = deathSprite;

        StartCoroutine(DestroySelf(1f));
    }
}
