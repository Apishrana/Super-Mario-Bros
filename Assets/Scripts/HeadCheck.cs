using UnityEngine;
using UnityEngine.Tilemaps;

public class HeadCheck : MonoBehaviour
{

    [SerializeField] private Transform headCheck;
    [SerializeField] private float headCheckRadius;
    [SerializeField] private LayerMask layer;
    public void checkHead()
    {
        Vector2 boxSize = new Vector2(headCheckRadius * 2, headCheckRadius);
        Collider2D[] hits = Physics2D.OverlapBoxAll(headCheck.position, boxSize, 0f, layer);

        if (hits.Length == 0)
        {
            return;
        }

        foreach (var hit in hits)
        {
            if (hit.gameObject == gameObject) continue;


            Tilemap tilemap = hit.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                Vector3Int cellPos = tilemap.WorldToCell(new Vector3(headCheck.position.x, headCheck.position.y + 1));
                tilemap.SetTile(cellPos, null);
            }

        }
    }
    // void OnDrawGizmosSelected()
    // {
    //     if (headCheck == null) return;
    //     Gizmos.color = Color.yellow;
    //     Vector3 boxSize = new Vector3(headCheckRadius * 2, headCheckRadius, 0);
    //     Gizmos.DrawWireCube(headCheck.position, boxSize);
    // }
}
