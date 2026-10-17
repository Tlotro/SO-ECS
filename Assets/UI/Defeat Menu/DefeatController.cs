using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class DefeatController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement root;
    
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        root.style.display = DisplayStyle.None;
        root.pickingMode = PickingMode.Ignore;
        root.SetEnabled(false);

        root.Q<Button>("MainMenuButton").RegisterCallback<ClickEvent>(e => { Time.timeScale = 1.0f; SceneManager.LoadScene("MainMenu"); });
    }

    public void ShowDefeatMenu()
    {
        root.style.display = DisplayStyle.Flex;
        root.pickingMode = PickingMode.Position;
        root.SetEnabled(true);

        Time.timeScale = 0.0f;
    }

}
