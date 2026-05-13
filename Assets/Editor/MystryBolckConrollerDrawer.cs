using UnityEditor;

[CustomEditor(typeof(MystryBolckConroller))]
public class MystryBolckConrollerDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SerializedProperty emptySprite = serializedObject.FindProperty("emptySprite");
        SerializedProperty bolckType = serializedObject.FindProperty("bolckType");
        SerializedProperty animationCurve = serializedObject.FindProperty("animationCurve");
        SerializedProperty coinCount = serializedObject.FindProperty("coinCount");
        SerializedProperty mushroomPrefab = serializedObject.FindProperty("mushroomPrefab");
        SerializedProperty starPrefab = serializedObject.FindProperty("starPrefab");
        EditorGUILayout.PropertyField(emptySprite);
        EditorGUILayout.PropertyField(bolckType);
        EditorGUILayout.PropertyField(animationCurve);
        MystryBolckConroller.BolckType type =
            (MystryBolckConroller.BolckType)bolckType.enumValueIndex;

        switch (type)
        {
            case MystryBolckConroller.BolckType.Coin:
                EditorGUILayout.PropertyField(coinCount);
                break;

            case MystryBolckConroller.BolckType.Mushroom:
                EditorGUILayout.PropertyField(mushroomPrefab);
                break;

            case MystryBolckConroller.BolckType.Star:
                EditorGUILayout.PropertyField(starPrefab);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
