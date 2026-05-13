using UnityEngine;
using UnityEngine.Tilemaps;

public class HeadCheck : MonoBehaviour
{

    [SerializeField] private Transform headCheck;
    [SerializeField] private float headCheckRadius;
    [SerializeField] private LayerMask layer;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float animationDuration = 0.25f;
    [SerializeField] private float moveDistance = 0.25f;
    private readonly System.Collections.Generic.HashSet<Vector3Int> animatingTiles = new();

    public void checkHead(bool grown)
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
            if (hit.gameObject.CompareTag("Mystry Block"))
            {
                hit.transform.GetComponent<MystryBolckConroller>().hit(this.GetComponent<PlayerMovement>());

            }
            else if (hit.gameObject.CompareTag("Breakable"))
            {

                Tilemap tilemap = hit.GetComponent<Tilemap>();
                if (tilemap != null)
                {
                    Vector3Int cellPos = tilemap.WorldToCell(new Vector3(headCheck.position.x, headCheck.position.y + 1));
                    if (tilemap.GetTile(cellPos))
                    {
                        if (grown)
                            tilemap.SetTile(cellPos, null);
                        else
                        {
                            if (!animatingTiles.Contains(cellPos))
                                StartCoroutine(AnimateTile(tilemap, cellPos));
                        }
                    }
                    else
                    {
                        cellPos = tilemap.WorldToCell(new Vector3(headCheck.position.x + 0.3f, headCheck.position.y + 1));
                        if (tilemap.GetTile(cellPos))
                        {
                            if (grown)
                                tilemap.SetTile(cellPos, null);
                            else
                            {
                                if (!animatingTiles.Contains(cellPos))
                                    StartCoroutine(AnimateTile(tilemap, cellPos));
                            }
                        }
                        else
                        {
                            cellPos = tilemap.WorldToCell(new Vector3(headCheck.position.x - 0.3f, headCheck.position.y + 1));
                            if (tilemap.GetTile(cellPos))
                            {
                                if (grown)
                                    tilemap.SetTile(cellPos, null);
                                else
                                {
                                    if (!animatingTiles.Contains(cellPos))
                                        StartCoroutine(AnimateTile(tilemap, cellPos));
                                }
                            }

                        }

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
    }

    private System.Collections.IEnumerator AnimateTile(Tilemap tilemap, Vector3Int cellPos)
    {
        Matrix4x4 originalMatrix = tilemap.GetTransformMatrix(cellPos);
        tilemap.SetTileFlags(cellPos, TileFlags.None);
        float time = 0f;
        animatingTiles.Add(cellPos);

        while (time < animationDuration)
        {
            time += Time.deltaTime;

            float normalizedTime = time / animationDuration;
            float curveValue = animationCurve.Evaluate(normalizedTime);

            Matrix4x4 matrix = Matrix4x4.TRS(
                Vector3.up * curveValue * moveDistance,
                Quaternion.identity,
                Vector3.one
            );

            tilemap.SetTransformMatrix(cellPos, originalMatrix * matrix);

            yield return null;
        }

        tilemap.SetTransformMatrix(cellPos, Matrix4x4.identity);
        tilemap.RefreshTile(cellPos);
        animatingTiles.Remove(cellPos);
    }
}