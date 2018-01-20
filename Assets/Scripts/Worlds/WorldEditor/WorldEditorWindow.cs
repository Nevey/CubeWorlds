using UnityEngine;
using UnityEditor;
using System;
using CCore.CubeWorlds.Worlds.WorldTiles;

namespace CCore.CubeWorlds.Worlds.Editor
{
    public class WorldEditorWindow : EditorWindow
    {
        private string worldName;

        private WorldTile tilePrefab;

        private int gridSize;

        private float spaceBetweenTiles;

        private bool useCustomPrefabs;

        private WorldEditorController worldEditorController = new WorldEditorController();

        [MenuItem("CCore/World Editor")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(WorldEditorWindow));
        }

        private void OnGUI()
        {
            minSize = new Vector2(300, 200);

            DrawTopHeader();

            DrawWorldInfo();
            
            DrawWorldSettings();

            DrawButtons();
        }

        private void DrawTopHeader()
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.Label("WORLD BUILDER", EditorStyles.largeLabel);

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.Space();
        }

        private void DrawWorldInfo()
        {
            EditorGUILayout.LabelField("World Info", EditorStyles.boldLabel);

            worldName = EditorGUILayout.TextField("World Name", worldName);

            EditorGUILayout.Space();
        }

        private void DrawWorldSettings()
        {
            EditorGUILayout.LabelField("World Settings", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            EditorGUILayout.BeginScrollView(Vector2.zero, GUILayout.Height(500));

            useCustomPrefabs = EditorGUILayout.BeginToggleGroup("Use Custom Prefabs", useCustomPrefabs);

            tilePrefab = (WorldTile)EditorGUILayout.ObjectField(tilePrefab, typeof(WorldTile), false);

            EditorGUILayout.EndToggleGroup();

            gridSize = EditorGUILayout.IntSlider("World Size", gridSize, 1, WorldEditorConstants.maxGridSize);

            spaceBetweenTiles = EditorGUILayout.Slider("Space Between Tiles", spaceBetweenTiles, 0, WorldEditorConstants.maxSpaceBetweenTiles);

            EditorGUILayout.Space();
        }

        private void DrawButtons()
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create", GUILayout.Width(100), GUILayout.Height(50)))
            {
                worldEditorController.TryCreateWorld(worldName, gridSize, tilePrefab, spaceBetweenTiles);
            }

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Save", GUILayout.Width(100), GUILayout.Height(50)))
            {
                worldEditorController.TrySaveWorldConfig(worldName);

                worldEditorController.TrySaveWorldPrefab(worldName);
            }

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }
    }
}