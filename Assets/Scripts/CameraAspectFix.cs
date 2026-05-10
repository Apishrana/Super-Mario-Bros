using UnityEngine;

public class CameraAspectFix : MonoBehaviour
{
    public float aspectRatioW;
    public float aspectRatioH;
    private float targetAspect;

    void Start()
    {
        targetAspect = aspectRatioW / aspectRatioH;
        Camera camera = GetComponent<Camera>();
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }
        transform.GetChild(0).position = new Vector2(-Camera.main.orthographicSize * Camera.main.aspect + transform.position.x, 0);
    }
}
