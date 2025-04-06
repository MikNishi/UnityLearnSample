using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class MoveObject : SampleScript
{
    [SerializeField]
    private Vector3 targetPosition = new Vector3(2, 2, 2);

    [SerializeField, Min(0.1f)]
    private float moveSpeed = 1f;

    [SerializeField, Min(-1)]
    private int repeatCount = 10;

    private Vector3 defaultPosition;
    private Transform myTransform;
    private bool toDefault;

    public UnityEvent onUseEvent;

    private void Awake()
    {
        myTransform = transform;
        defaultPosition = myTransform.position;
        toDefault = false;
    }

    [ContextMenu("Начать выполнение")]
    public override void Use()
    {
        StopAllCoroutines();
        StartCoroutine(RepeatMove());
        onUseEvent.Invoke();
    }

    private IEnumerator RepeatMove()
    {
        int count = 0;
        while (repeatCount < 0 || count < repeatCount)
        {
            Vector3 target = toDefault ? defaultPosition : targetPosition;
            yield return StartCoroutine(MoveCoroutine(target));
            toDefault = !toDefault;
            count++;
        }
    }

    private IEnumerator MoveCoroutine(Vector3 target)
    {
        while (Vector3.Distance(myTransform.position, target) > 0.01f)
        {
            Vector3 direction = (target - myTransform.position).normalized;
            myTransform.position += direction * moveSpeed * Time.deltaTime;
            yield return null;
        }

        myTransform.position = target; // Гарантия точного попадания
    }
}