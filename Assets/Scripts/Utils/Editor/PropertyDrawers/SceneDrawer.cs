using UnityEditor;
using UnityEngine;

namespace KidsTest.Utils
{
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public class SceneDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            if (property.propertyType == SerializedPropertyType.String)
            {
                if (GetSceneObject(property.stringValue, out SceneAsset sceneObject))
                {
                    var scene = EditorGUI.ObjectField(position, label, sceneObject, typeof(SceneAsset), true);
                    if (scene is null)
                        property.stringValue = string.Empty;
                    else if (scene.name != property.stringValue)
                    {
                        if (GetSceneObject(scene.name, out SceneAsset sceneObj))
                            property.stringValue = scene.name;
                        else
                            Debug.LogWarning($"The scene {scene.name} cannot be used. To use this scene add it to the build settings for the project");
                    }
                }
            }
            else
                EditorGUI.LabelField(position, label.text, "Use [Scene] with strings.");
        }

        protected bool GetSceneObject(string sceneObjectName, out SceneAsset scene)
        {
            scene = null;

            if (string.IsNullOrEmpty(sceneObjectName))
                return false;

            foreach (var editorScene in EditorBuildSettings.scenes)
            {
                if (editorScene.path.IndexOf(sceneObjectName) != -1)
                {
                    scene = AssetDatabase.LoadAssetAtPath(editorScene.path, typeof(SceneAsset)) as SceneAsset;
                    return true; 
                }
            }

            Debug.LogWarning($"Scene [{sceneObjectName}] cannot be used. Add this scene to the 'Scenes in the Build' in build settings.");
            
            return false;
        }
    }
}
