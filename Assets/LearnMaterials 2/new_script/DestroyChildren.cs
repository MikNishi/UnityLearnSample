using UnityEngine;
using System.Collections;

public class DestroyChildren : SampleScript
{
    [SerializeField]
    private Transform target;

    [SerializeField, Min(0.1f)]
    private float shrinkDuration = 1f;

    private Transform myTransform;

    private void Awake()
    {
        myTransform = target ? target : transform;
    }

    [ContextMenu("Начать выполнение")]
    public override void Use()
    {
        StopAllCoroutines();
        StartCoroutine(DestroyChildrenCoroutine());
    }

    private IEnumerator DestroyChildrenCoroutine()
    {
        while (myTransform.childCount > 0)
        {
            Transform child = myTransform.GetChild(0);
            if (child != null)
            {
                yield return StartCoroutine(ShrinkAndDestroy(child));
            }
            yield return null; 
        }
    }

    private IEnumerator ShrinkAndDestroy(Transform obj)
    {
        Vector3 originalScale = obj.localScale;
        float timeElapsed = 0f;

        while (timeElapsed < shrinkDuration)
        {
            float normalizedTime = timeElapsed / shrinkDuration; 
            obj.localScale = Vector3.Lerp(originalScale, Vector3.zero, normalizedTime);
            timeElapsed += Time.deltaTime; 
            yield return null;
        }
        
        obj.localScale = Vector3.zero;
        Destroy(obj.gameObject);
    }
}