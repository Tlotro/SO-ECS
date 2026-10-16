using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MyUIController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement root;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        root.Q<Button>("PlayButton").RegisterCallback<ClickEvent>(e => SceneManager.LoadScene("GameplayScene"));
        root.Q<Button>("SettingsButton").RegisterCallback<ClickEvent>(e => Debug.Log("No settings there."));
        root.Q<Button>("ExitButton").RegisterCallback<ClickEvent>(e => Debug.Log("Application quit, I suppose."));
    }

}