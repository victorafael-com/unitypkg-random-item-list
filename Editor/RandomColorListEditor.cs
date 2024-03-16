using UnityEditor;
namespace com.victorafael.randomList
{
    [CustomEditor(typeof(RandomColorList))]
    public class RandomColorListEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RandomListDataEditor.Draw(serializedObject, target as RandomColorList);
        }
    }
}