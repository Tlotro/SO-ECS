using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UpgradeController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement root;

    private readonly List<Toggle> m_Options = new List<Toggle>();
    private int m_SelectedIndex = -1;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        SetUpgradeMenuVisible(false);

        var group = root.Q("UpgradeGroup");

        group.Query<Toggle>(className: "upgrade-option").ForEach(t =>
        {
            int index = m_Options.Count;
            m_Options.Add(t);

            t.RegisterValueChangedCallback(evt =>
            {
                if (evt.newValue) Select(index);
                else t.SetValueWithoutNotify(true);
            });
        });

        root.Q<Button>("ConfirmButton").clicked += OnConfirm;

        if (m_Options.Count > 0) Select(0);
    }

    private void Select(int index)
    {
        for (int i = 0; i < m_Options.Count; i++)
        {
            bool isSelected = i == index;
            m_Options[i].SetValueWithoutNotify(isSelected);
            m_Options[i].EnableInClassList("selected", isSelected);
        }
        m_SelectedIndex = index;
    }

    private void SetUpgradeMenuVisible(bool visible)
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

    public void ShowUpgradeMenu(List<string> upgrades)
    {
        for (int i = 0; i < 3; i++)
            m_Options[i].label = upgrades[i];
        
        Select(0);
        SetUpgradeMenuVisible(true);
        Time.timeScale = 0.0f;

    }

    private void OnConfirm()
    {
        if (m_SelectedIndex < 0) return;

        SetUpgradeMenuVisible(false);
        Time.timeScale = 1.0f;

        Debug.Log($"Upgrade #{m_SelectedIndex}");
    }

}
