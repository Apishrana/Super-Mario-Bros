using System.Collections;
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
    [SerializeField]
    private float animationDuration = 0.25f;
    [SerializeField]
    private float moveDistance = 0.5f;
    private bool isAnimating;
    void Start()
    {
        if (sprite == null)
        {
            sprite = transform.GetChild(0);
        }
    }
    public void hit(PlayerMovement player)
    {
        if (isAnimating)
        {
            return;
        }

        StartCoroutine(AnimateBlock());

        switch (bolckType)
        {
            case BolckType.Coin:
                Debug.Log(1);
                break;

            case BolckType.Mushroom:
                Debug.Log(1);
                break;

            case BolckType.Star:
                Debug.Log(1);
                break;

        }
    }
    private IEnumerator AnimateBlock()
    {
        isAnimating = true;
        Vector3 startPos = sprite.localPosition;
        float time = 0f;

        while (time < animationDuration)
        {
            time += Time.deltaTime;
            float normalizedTime = time / animationDuration;
            float curveValue = animationCurve.Evaluate(normalizedTime);
            sprite.localPosition = startPos + Vector3.up * curveValue * moveDistance;
            yield return null;
        }
        sprite.localPosition = startPos;
        isAnimating = false;
    }
    void OnDestroy()
    {
        Destroy(transform.GetComponent<Animation>());
        Destroy(transform.GetComponent<Animator>());
        sprite.GetComponent<SpriteRenderer>().sprite = emptySprite;
    }
}