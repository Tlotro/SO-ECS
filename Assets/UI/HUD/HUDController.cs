using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HUDController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement root;

    private VisualElement healthbar;
    private VisualElement expBar;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        healthbar = root.Q<VisualElement>("HealthbarFill");
        expBar = root.Q<VisualElement>("ExpBar");
    }

    public void SetHealth(float cur, float max)
    {
        healthbar.style.width = Length.Percent(Mathf.Clamp(cur / max, 0, 1) * 100f);
    }

    public void SetExp(float cur, float max)
    {
        expBar.style.width = Length.Percent(Mathf.Clamp(cur / max, 0, 1) * 100f);
    }

}
