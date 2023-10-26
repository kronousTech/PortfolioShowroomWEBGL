using UnityEngine;

public static class TransformExtensions
{
    public static Transform DisableChildren(this Transform current)
    {
        for (int i = current.childCount - 1; i >= 0; i--)
        {
            current.GetChild(i).gameObject.SetActive(false);
        }
        return current;
    }
    public static Transform ClearChildren(this Transform current)
    {
        return current.ClearChildrenHavingName(null);
    }
    public static Transform ClearChildrenHavingName(this Transform current, string OnlyWithName)
    {
        GameObject go;
        bool remove;
        for (int i = current.childCount - 1; i >= 0; i--)
        {
            go = current.GetChild(i).gameObject;
            remove = (OnlyWithName == null || go.name == OnlyWithName);
            if (remove)
            {
                go.transform.SetParent(null);
#if UNITY_EDITOR
                if (UnityEditor.EditorApplication.isPlaying)
                    GameObject.Destroy(go);
                else GameObject.DestroyImmediate(go);
#else
                    GameObject.Destroy(go);
#endif
            }
        }
        return current;
    }
    public static T FindChildRecursiveComp<T>(this Transform current, string name)
    {
        Transform child = current.FindChildRecursive(name);
        return child.GetComponent<T>();
    }

    public static Transform FindChildRecursive(this Transform current, string name)
    {
        // check if the current bone is the bone we're looking for, if so return it
        if (current.name == name)
            return current;
        // search through child bones for the bone we're looking for
        Transform[] trs = current.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t == current) continue;
            // the recursive step; repeat the search one step deeper in the hierarchy
            Transform found = t.FindChildRecursive(name);
            // a transform was returned by the search above that is not null,
            // it must be the bone we're looking for
            if (found != null)
                return found;
        }

        // bone with name was not found
        return null;
    }
}
