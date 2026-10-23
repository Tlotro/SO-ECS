using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class PauseController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement root;

    private bool paused = false;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        SetPauseMenuVisible(false);

        root.Q<Button>("ResumeButton").RegisterCallback<ClickEvent>(e => Pause());
        root.Q<Button>("ExitButton").RegisterCallback<ClickEvent>(e => { Time.timeScale = 1.0f; SceneManager.LoadScene("MainMenu"); } );
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Pause();
        }
    }

    private void Pause()
    {
        if (!paused && Time.timeScale == 0) return;
        paused = !paused;
        SetPauseMenuVisible(paused);
        Time.timeScale = paused ? 0f : 1f;
    }

    private void SetPauseMenuVisible(bool visible)
    {
        if (visible)
        {
            root.style.display = DisplayStyle.Flex;
            root.pickingMode = PickingMode.Position;
            root.SetEnabled(true);
        }
        else
        {
            root.style.display = DisplayStyle.None;
            root.pickingMode = PickingMode.Ignore;
            root.SetEnabled(false);
        }
    }

}