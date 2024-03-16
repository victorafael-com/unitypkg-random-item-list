using UnityEditor;
namespace com.victorafael.randomList
{
    [CustomEditor(typeof(RandomPrefabList))]
    public class RandomPrefabListEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RandomListDataEditor.Draw(serializedObject, target as RandomPrefabList);
        }
    }
}