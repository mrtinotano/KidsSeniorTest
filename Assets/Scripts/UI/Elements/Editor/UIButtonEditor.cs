using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace KidsTest
{
    [CustomEditor(typeof(UIButton), true)]
    [CanEditMultipleObjects]
    public class UIButtonEditor : ButtonEditor
    {
        private SerializedProperty m_PressPunch;
        private SerializedProperty m_PunchTime;
        private SerializedProperty m_ButtonSound;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_PressPunch = serializedObject.FindProperty("m_PressPunch");
            m_PunchTime = serializedObject.FindProperty("m_PunchTime");
            m_ButtonSound = serializedObject.FindProperty("m_ButtonSound");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_PressPunch);
            EditorGUILayout.PropertyField(m_PunchTime);

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(m_ButtonSound);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
