using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    [SerializeField] private GameObject prefab; 
    private InteractiveBox selectedBox;        

    private Camera cam; 

    private void Start()
    {
        cam = Camera.main; 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // À Ã
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("InteractivePlane"))
                {
                    Vector3 spawnPos = hit.point + hit.normal * (prefab.transform.localScale.y / 2f);
                    GameObject newBox = Instantiate(prefab, spawnPos, Quaternion.identity);
                }

                InteractiveBox hitBox = hit.collider.GetComponent<InteractiveBox>();
                if (hitBox != null)
                {
                    if (selectedBox == null)
                    {
                        selectedBox = hitBox;
                    }
                    else
                    {
                        if (hitBox != selectedBox)
                        {
                            selectedBox.AddNext(hitBox);
                            selectedBox = null; 
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) // œ Ã
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                InteractiveBox hitBox = hit.collider.GetComponent<InteractiveBox>();
                if (hitBox != null)
                {
                    Destroy(hitBox.gameObject); 
                    if (selectedBox == hitBox)
                        selectedBox = null;
                }
            }
        }
    }
}
