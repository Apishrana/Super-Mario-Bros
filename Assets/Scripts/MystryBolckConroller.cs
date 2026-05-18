using System.Collections;
using UnityEngine;

public class MystryBolckConroller : MonoBehaviour
{
    [SerializeField]
    private Sprite emptySprite;
    [SerializeField]
    private Sprite defaultSprite;
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
    [SerializeField]
    private bool block = true;
    private bool isAnimating;
    void Start()
    {
        if (sprite == null)
        {
            sprite = transform.GetChild(0);
        }
    }
    public IEnumerator hit(PlayerMovement player)
    {
        if (isAnimating)
        {
            yield break;
        }

        yield return StartCoroutine(AnimateBlock());
        if (block)
        {
            transform.GetComponent<Animation>().enabled = true;
            transform.GetComponent<Animator>().enabled = true;
        }
        else
        {
            sprite.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        }
        switch (bolckType)
        {
            case BolckType.Coin:
                player.coinCount++;
                coinCount--;
                if (coinCount == 0)
                {
                    Destroy(this);
                }
                break;

            case BolckType.Mushroom:
                if (mushroomPrefab != null)
                {
                    Instantiate(mushroomPrefab, transform.position + Vector3.up, Quaternion.identity);
                }
                Destroy(this);
                break;

            case BolckType.Star:
                if (starPrefab != null)
                {
                    Instantiate(starPrefab, transform.position + Vector3.up, Quaternion.identity);
                }
                Destroy(this);
                break;

        }
    }
    private IEnumerator AnimateBlock()
    {
        if (block)
        {
            transform.GetComponent<Animation>().enabled = false;
            transform.GetComponent<Animator>().enabled = false;
        }
        sprite.GetComponent<SpriteRenderer>().sprite = emptySprite;
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
        if (block)

        {
            Destroy(transform.GetComponent<Animation>());
            Destroy(transform.GetComponent<Animator>());
        }
        sprite.GetComponent<SpriteRenderer>().sprite = emptySprite;
    }
}