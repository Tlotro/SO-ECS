using System;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Timeline.DirectorControlPlayable;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Game/Ability Data")]
public class AbilitySO : ScriptableObject
{
    public AbilitySO nextLevel;
    public float abilityCooldown;
    [SerializeReference]
    private IAbility _ability;
    private CancellationTokenSource cts;

    public void ActivateAbility(PlayerController player) => _ability?.ActivateAbility(player);

    public async void Run()
    {
        cts = new CancellationTokenSource();
        
        try
        {
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                ActivateAbility(GlobalDataSO.Instance.player.GetComponent<PlayerController>());
                await Awaitable.WaitForSecondsAsync(abilityCooldown, CancellationToken.None);
            }
        }
        catch (OperationCanceledException) { }
    }

    public void Stop()
    {
        cts?.Cancel();
        cts?.Dispose();
    }
}

public interface IAbility
{
    void ActivateAbility(PlayerController player) { }
}

public sealed class ArrowSkill : IAbility
{
    [SerializeField]
    ProjectileSO _projectile;

    public void ActivateAbility(PlayerController player)
    {
        GameObject arrow = Projectile.CreateProjectile(_projectile);
        Vector2 mousepos = Camera.main.GetComponent<CameraFollow>().mouseAction.action.ReadValue<Vector2>();
        arrow.transform.position = player.transform.position;
        arrow.transform.up = Camera.main.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y,-Camera.main.transform.position.z)) - player.gameObject.transform.position;
        arrow.GetComponent<MovementController>().Direction = arrow.transform.up;
    }
}

public sealed class LightningSkill : IAbility
{

    public void ActivateAbility(PlayerController player)
    {
        // Lightning
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AbilitySO))]
public sealed class SkillEditor : Editor
{
    private SerializedProperty _skillProperty;
    private GUIContent _skillTypeContent;
    private Type[] _skillTypes;

    private void OnEnable()
    {
        var so = this.serializedObject;
        _skillProperty = so.FindProperty("_ability");
        UpdateDropdownContent();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "_ability");

        if (EditorGUILayout.DropdownButton(_skillTypeContent, FocusType.Keyboard))
        {
            OpenSkillSelector();
        }

        var skill = _skillProperty.managedReferenceValue;
        if (skill != null)
        {
            EditorGUILayout.PropertyField(_skillProperty, _skillTypeContent, includeChildren: true);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OpenSkillSelector()
    {
        var genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("None"), _skillProperty.managedReferenceValue == null, () =>
        {
            _skillProperty.managedReferenceValue = null;
            serializedObject.ApplyModifiedProperties();
            UpdateDropdownContent();
        });

        var types = TypeCache.GetTypesDerivedFrom<IAbility>();
        foreach (var type in types)
        {
            genericMenu.AddItem(new GUIContent(type.Name), false, UpdateSelectedType, type);
        }

        genericMenu.ShowAsContext();
    }

    private void UpdateSelectedType(System.Object obj)
    {
        var type = (Type)obj;
        var constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor != null)
        {
            var value = constructor.Invoke(null);
            _skillProperty.managedReferenceValue = value;
            this.serializedObject.ApplyModifiedProperties();
            UpdateDropdownContent();
        }
    }

    private void UpdateDropdownContent()
    {
        var value = _skillProperty.managedReferenceValue;
        _skillTypeContent = value != null ? new GUIContent(value.GetType().Name) : new GUIContent("Select Ability");
    }
}
#endif
