using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;



[CustomEditor(typeof(ExtractMaterial))]
public class ExtractMaterialEditor : Editor
{

    public override void OnInspectorGUI()
    {
        
        if (GUILayout.Button("Extract Material"))
        {
            ExtractMaterial extractMaterial = (target as ExtractMaterial);
            extractMaterial.Extract();
        }
    }
}

public class ExtractMaterial : MonoBehaviour {

	// Use this for initialization
	public void Extract()
    {


        string modelPath = EditorUtility.OpenFilePanel("Select model", "", "fbx");

        if (modelPath.StartsWith(Application.dataPath))
        {
            modelPath = "Assets" + modelPath.Substring(Application.dataPath.Length);
        }

        Object model = AssetDatabase.LoadAssetAtPath(modelPath, typeof(Object)) as Object;

        try
        {
            AssetDatabase.StartAssetEditing();

            var assetsToReload = new HashSet<string>();

            var materials = AssetDatabase.LoadAllAssetsAtPath(modelPath).Where(x => x.GetType() == typeof(Material));

            foreach (var material in materials)
            {
                Material m = material as Material;
                if (m != null)
                {
                    m.color = Color.white;
                }
                var newAssetPath = modelPath.Substring(0, modelPath.Length - model.name.Length - 4) + material.name + ".mat"; // -4 is for .fbx

                newAssetPath = AssetDatabase.GenerateUniqueAssetPath(newAssetPath);
                var error = AssetDatabase.ExtractAsset(material, newAssetPath);
                if (string.IsNullOrEmpty(error))
                {
                    assetsToReload.Add(modelPath);
                }
            }

            foreach (var path in assetsToReload)
            {
                AssetDatabase.WriteImportSettingsIfDirty(path);
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
        }
    }
}
