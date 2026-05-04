using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public float CamY;
    public float CamX;
    [SerializeField] private float CamMinX;
    [SerializeField] private float CamMaxX;
    [SerializeField] private Transform player;
    void Start()
    {
        CamY = transform.position.y;
        CamX = CamMinX;
    }
    void Update()
    {
        CamX = player.position.x;
        if (CamX < CamMinX)
        {
            CamX = CamMinX;
        }
        if (CamX > CamMaxX)
        {
            CamX = CamMaxX;
        }
        transform.position = new Vector3(CamX, CamY, -10);
        CamMinX = CamX;
    }
}
