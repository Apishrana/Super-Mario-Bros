using UnityEngine;

public class Pipe : MonoBehaviour
{
    public enum PipeType { Vertical, Horizontal }
    public PipeType pipeType;
    public Vector2 tpPos;
    public float camY;
    public float camMinX;
    public float camMaxX;
}
