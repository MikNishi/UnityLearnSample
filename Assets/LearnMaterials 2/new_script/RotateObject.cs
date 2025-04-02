using UnityEngine;
using System.Collections;


public class RotateObject : SampleScript
{
    [SerializeField]
    private Vector3 rotationAngles;

    [SerializeField, Min(0.1f)]
    private float rotationSpeed = 10f;

    [SerializeField, Min(-1)]
    private int repeatCount = 10;

    private Quaternion defaultRotation;
    private Transform myTransform;
    private bool toDefault;

    private void Awake()
    {
        myTransform = transform;
        defaultRotation = myTransform.rotation;
        toDefault = false;
    }

    [ContextMenu("Начать выполнение")]
    public override void Use()
    {
        StopAllCoroutines();
        StartCoroutine(RepeatRotate());
    }

    private IEnumerator RepeatRotate()
    {
        int count = 0;
        while (repeatCount < 0 || count < repeatCount)
        {
            Quaternion targetRotation = toDefault ? defaultRotation : defaultRotation * Quaternion.Euler(rotationAngles);
            yield return StartCoroutine(RotateCoroutine(targetRotation));
            toDefault = !toDefault;
            count++;
        }
    }

    private IEnumerator RotateCoroutine(Quaternion targetRotation)
    {
        if (myTransform == null)
        {
            Debug.LogError("myTransform is null!");
            yield break;
        }

        while (Quaternion.Angle(myTransform.rotation, targetRotation) > 0.1f)
        {
            myTransform.rotation = Quaternion.RotateTowards(myTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        myTransform.rotation = targetRotation; // Гарантия точного попадания
    }
}
