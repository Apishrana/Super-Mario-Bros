using UnityEngine;

public class MystryBolckConroller : MonoBehaviour
{
    [SerializeField]
    private Sprite emptySprite;
    [SerializeField]
    private Transform sprite;
    public enum BolckType { Coin, Mushroom, Star };
    [SerializeField]
    private BolckType bolckType;
    [SerializeField]
    private AnimationCurve animationCurve;
    [SerializeField]
    private int coinCount;
    [SerializeField]
    private GameObject mushroomPrefab;
    [SerializeField]
    private GameObject starPrefab;
    void Start()
    {
        if (sprite == null)
        {
            sprite = transform.GetChild(0);
        }
    }
    public void hit(PlayerMovement player)
    {
        Debug.Log(player);
    }
}