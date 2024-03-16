# Random List Item Package

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/your-username/your-repo/blob/main/LICENSE)

A simple package that provides a utility function to retrieve a random item from a list.

## Installation

To install this project use [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui.html), following those steps:

1. Open Unity Package on `Window/Package Manager`;
2. Click the + button;
3. Choose `Add package from git URL...`;
4. Add the url `https://github.com/victorafael-com/unitypkg-random-item-list.git`

## Creating custom lists

The random list can be used with any serializable value, but the custom editor will not handle well foldable content (such as Serialized classes)

To be able to create Random Lists to your custom scriptable class, let's say **Enemy** you must create two files:

**RandomEnemyList.cs**

```cpp, RandomEnemyList.cs
using com.victorafael.randomList;
using UnityEngine;

[CreateAssetMenu(menuName = "Random Enemy List")]
public class RandomEnemyList : RandomListData<Enemy> { }
```

**Editor/RandomEnemyListEditor.cs**

```cpp, Editor/RandomEnemyListEditor.cs
using UnityEditor;
using com.victorafael.randomList;

[CustomEditor(typeof(RandomEnemyList))]
public class RandomEnemyListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RandomListDataEditor.Draw(serializedObject, target as RandomEnemyList);
    }
}
```
