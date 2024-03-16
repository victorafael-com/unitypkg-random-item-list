using UnityEditor;
namespace com.victorafael.randomList
{
    [CustomEditor(typeof(RandomStringList))]
    public class RandomStringListEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RandomListDataEditor.Draw(serializedObject, target as RandomStringList);
        }
    }
}