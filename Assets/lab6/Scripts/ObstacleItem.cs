using UnityEngine;
using UnityEngine.Events;

public class ObstacleItem : MonoBehaviour
{
    [Range(0, 1)]
    public float currentValue = 1f; // здоровье

    public UnityEvent onDestroyObstacle = new UnityEvent(); // событие удаления

    private Renderer objRenderer;
    private Color initColor;

    private void Awake()
    {
        objRenderer = GetComponent<Renderer>();
        initColor = Color.white;
        UpdateColor();
    }

    public void SubscribeToDestroy(UnityAction action)
    {
        onDestroyObstacle.AddListener(action);
    }

    public void GetDamage(float damage)
    {
        currentValue -= damage;

        UpdateColor();

        if (currentValue <= 0)
        {
            onDestroyObstacle.Invoke();
            Destroy(gameObject);
        }
    }

    private void UpdateColor()
    {
        if (objRenderer != null)
        {
            Color newColor = Color.Lerp(Color.red, Color.white, currentValue);
            objRenderer.material.color = newColor;
        }
    }
}

