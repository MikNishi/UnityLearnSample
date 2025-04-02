using UnityEngine;

public class CloneObject : SampleScript
{
    [SerializeField, Min(1)] 
    private int count = 3;

    [SerializeField] 
    private Vector3 step = new Vector3(1, 1, 1);


    [ContextMenu("Начать выполнение")]
    public override void Use()
    {
        for (int i = 1; i <= count; i++)
        {
            Instantiate(gameObject, transform.position + step * i, Quaternion.identity);
        }
    }
}
