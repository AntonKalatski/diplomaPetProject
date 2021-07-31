using UnityEditor;
using UnityEngine;

namespace Utils
{
    public class CreateSubProjectWindow : EditorWindow
    {
        string projectName = string.Empty;
        [MenuItem("Assets/Create/Create subproject folder group")]
        static void Init()
        {
            var window = GetWindow<CreateSubProjectWindow>();
            window.Show();
        }
        void OnGUI()
        {
            EditorGUI.DropShadowLabel(new Rect(0, 0, position.width, 20), "Name your subproject");
            projectName = EditorGUI.TextField(new Rect(10, 25, position.width - 20, 20), "Subproject name:", projectName);

            if (GUI.Button(new Rect(0, 50, position.width, 30), "Create!"))
            {
                CreateSubprojectFolders();
                this.Close();
            }
        }

        private void CreateSubprojectFolders()
        {
            string subProject = AssetDatabase.CreateFolder("Assets", projectName);
            string subProjectPath = AssetDatabase.GUIDToAssetPath(subProject);
            AssetDatabase.CreateFolder(subProjectPath, "Scripts");
            AssetDatabase.CreateFolder(subProjectPath, "Shaders");
            AssetDatabase.CreateFolder(subProjectPath, "Scenes");
            AssetDatabase.CreateFolder(subProjectPath, "Materials");
            AssetDatabase.CreateFolder(subProjectPath, "Prefabs");
            AssetDatabase.CreateFolder(subProjectPath, "Textures");
        }
    }
}
