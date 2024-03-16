using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace com.victorafael.randomList
{
	public static class RandomListDataEditor
	{

		private static int maxScoreTest;
		private static int testScore = 0;

		public static void Draw<T>(SerializedObject serialized, RandomListData<T> data)
		{
			//var data = targetObject as RandomListData;

			var objWidth = GUILayout.Width(105);
			var valWidth = GUILayout.Width(30);
			var bigValWidth = GUILayout.Width(60);
			var buttonWidth = GUILayout.Width(20);

			EditorGUI.BeginChangeCheck();

			GUILayout.Label("Items", EditorStyles.boldLabel);

			EditorGUILayout.BeginHorizontal();

			string name = typeof(T).ToString();

			GUILayout.Label(name.Substring(name.LastIndexOf(".") + 1), objWidth);
			GUILayout.Label("start", valWidth);
			GUILayout.Label("increment", bigValWidth);
			GUILayout.Label("min", valWidth);
			GUILayout.Label("max", valWidth);
			GUILayout.Label("actions");

			EditorGUILayout.EndHorizontal();

			if (data.items == null)
			{
				data.items = new List<RandomListData<T>.RandomListDataItem>();
			}

			var items = serialized.FindProperty("items");
			if (items == null)
			{
				//SerializedProperty p = serialized.ApplyModifiedProperties();
			}

			float[] chances;
			float chanceSum = 1;
			if (items.arraySize > 0)
			{
				chances = data.GetChances(testScore, out chanceSum);
			}
			else
			{
				chances = new float[0];
			}

			for (int i = 0; i < items.arraySize; i++)
			{
				var item = items.GetArrayElementAtIndex(i);

				EditorGUILayout.BeginHorizontal();

				EditorGUILayout.PropertyField(item.FindPropertyRelative("item"), GUIContent.none, objWidth);
				EditorGUILayout.PropertyField(item.FindPropertyRelative("startChance"), GUIContent.none);
				EditorGUILayout.PropertyField(item.FindPropertyRelative("scoreMultiplier"), GUIContent.none);
				EditorGUILayout.PropertyField(item.FindPropertyRelative("minChance"), GUIContent.none);
				EditorGUILayout.PropertyField(item.FindPropertyRelative("maxChance"), GUIContent.none);

				GUI.enabled = i > 0;
				if (GUILayout.Button("↑", buttonWidth))
				{
					items.MoveArrayElement(i, i - 1);
					break;
				}
				GUI.enabled = i < items.arraySize - 1;
				if (GUILayout.Button("↓", buttonWidth))
				{
					items.MoveArrayElement(i, i + 1);
					break;
				}

				GUI.enabled = true;
				if (GUILayout.Button("x", buttonWidth))
				{
					items.DeleteArrayElementAtIndex(i);
					break;
				}

				EditorGUILayout.EndHorizontal();
				var pctRect = EditorGUILayout.GetControlRect(GUILayout.Height(5));
				GUI.color = Color.black;
				GUI.DrawTexture(pctRect, EditorGUIUtility.whiteTexture);
				GUI.color = Color.white;
				pctRect.x += 1;
				pctRect.y += 1;
				pctRect.width -= 2;
				pctRect.height -= 2;
				pctRect.width *= chances[i] / chanceSum;
				GUI.DrawTexture(pctRect, EditorGUIUtility.whiteTexture);
			}

			if (items.arraySize == 0)
				GUILayout.Label("-- No item --");

			if (GUILayout.Button("+"))
			{
				items.arraySize++;
				var newItem = items.GetArrayElementAtIndex(items.arraySize - 1);
				newItem.FindPropertyRelative("startChance").floatValue = 5;
				newItem.FindPropertyRelative("scoreMultiplier").floatValue = 0.2f;
				newItem.FindPropertyRelative("minChance").floatValue = 0;
				newItem.FindPropertyRelative("maxChance").floatValue = 100;
			}

			if (EditorGUI.EndChangeCheck())
			{
				serialized.ApplyModifiedProperties();
				EditorUtility.SetDirty(data);
			}


			EditorGUILayout.Space();
			GUILayout.Label("Preview", EditorStyles.boldLabel);

			EditorGUI.BeginChangeCheck();
			maxScoreTest = EditorGUILayout.IntField("max Score", EditorPrefs.GetInt("maxScoreValue", 1000));
			if (EditorGUI.EndChangeCheck())
			{
				EditorPrefs.SetInt("maxScoreValue", maxScoreTest);
			}

			EditorGUILayout.Space();
			testScore = EditorGUILayout.IntSlider("score", testScore, 0, maxScoreTest);
		}
	}
}