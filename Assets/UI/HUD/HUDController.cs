using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HUDController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement root;

    private VisualElement healthbar;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        healthbar = root.Q<VisualElement>("HealthbarFill");
    }

    public void SetHealth(float cur, float max)
    {
        healthbar.style.width = Length.Percent(Mathf.Clamp(cur / max, 0, 1) * 100f);
    }

}
