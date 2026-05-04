using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HeadCheck : MonoBehaviour
{

    [SerializeField] private Transform headCheck;
    [SerializeField] private float headCheckRadius;
    [SerializeField] private LayerMask layer;
    public void checkHead()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(headCheck.position, headCheckRadius, layer);

        if (hits.Length == 0)
        {
            Debug.LogWarning("Nothing hit");
            return;
        }

        foreach (var hit in hits)
        {
            if (hit.gameObject == gameObject) continue;

            Debug.Log("Hit: " + hit.name);

            Tilemap tilemap = hit.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                Vector3Int cellPos = tilemap.WorldToCell(new Vector3(headCheck.position.x, headCheck.position.y + 1));
                tilemap.SetTile(cellPos, null);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (headCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(headCheck.position, headCheckRadius);
    }
}
