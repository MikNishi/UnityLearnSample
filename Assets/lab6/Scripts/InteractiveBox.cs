using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    private InteractiveBox next;

    public void AddNext(InteractiveBox box)
    {
        next = box;
    }

    private void Update()
    {
        if (next == null) return;

        Vector3 direction = next.transform.position - transform.position;
        RaycastHit hit;


        if (Physics.Raycast(transform.position, direction.normalized, out hit, direction.magnitude))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);


            ObstacleItem obstacle = hit.collider.GetComponent<ObstacleItem>();
            if (obstacle != null)
            {
                obstacle.GetDamage(Time.deltaTime);
            }
        }
        else
        {
            Debug.DrawLine(transform.position, next.transform.position, Color.green);
        }
    }
}