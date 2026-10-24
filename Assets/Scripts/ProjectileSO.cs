using UnityEditor;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Game/Projectile Data")]
public class ProjectileSO : ScriptableObject
{
    public float damage = 0;
    public float Speed = 0;
    public bool ignoreWalls;
    public float MaxLifetime;
    public Sprite ProjectileSprite;
    [SerializeReference]
    private IProjectile _projectile;

    public void OnHit(Projectile self, Enemy hit) => _projectile?.OnHit(self, hit);
    public void OnWallHit(Projectile self) => _projectile?.OnWallHit(self);
    public void start(Projectile self) => _projectile.Start(self);
    public void update(Projectile self) => _projectile.Update(self);
}

public interface IProjectile
{
    void OnHit(Projectile self, Enemy hit) { self.Kill(); }

    void OnWallHit(Projectile self) { self.Kill(); }
    void Update(Projectile self) { }
    void Start(Projectile self) { }
}

public sealed class ArrowProjectile : IProjectile
{ 

}
public sealed class BoomerangProjectile : IProjectile
{
    void IProjectile.Update(Projectile self)
    {
        self.gameObject.transform.Rotate(Vector3.forward, Time.deltaTime*360);
        self.movementController.Direction = self.gameObject.transform.up;
    }

    void IProjectile.OnHit(Projectile self, Enemy hit)
    {
        //hit.GetComponent<HPController>().immune.Add(self.gameObject);
    }
}

public sealed class BoneProjectile : IProjectile
{
    void IProjectile.Update(Projectile self)
    {
        self.extraData[0] = Quaternion.AngleAxis(360*Time.deltaTime, Vector3.forward) * (Vector3)self.extraData[0];
        self.transform.position = GlobalDataSO.Instance.player.transform.position + (Vector3)self.extraData[0];
        self.gameObject.transform.Rotate(Vector3.forward, -Time.deltaTime * 1800);
    }

    void IProjectile.OnHit(Projectile self, Enemy hit)
    {
        //hit.GetComponent<HPController>().immune.Add(self.gameObject);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ProjectileSO))]
public sealed class ProjectileEditor : Editor
{
    private SerializedProperty _skillProperty;
    private GUIContent _skillTypeContent;

    private void OnEnable()
    {
        var so = this.serializedObject;
        _skillProperty = so.FindProperty("_projectile");
        UpdateDropdownContent();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "_projectile");

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

        var types = TypeCache.GetTypesDerivedFrom<IProjectile>();
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
        _skillTypeContent = value != null ? new GUIContent(value.GetType().Name) : new GUIContent("Select Projectile");
    }
}
#endif

