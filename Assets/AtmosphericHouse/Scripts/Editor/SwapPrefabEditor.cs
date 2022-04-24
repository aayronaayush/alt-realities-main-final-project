﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;

//*****  (©) Finward Studios 2021. All rights reserved. *****\\

namespace PrefabSwapper
{
    [CustomEditor(typeof(SwapPrefab))]
    [CanEditMultipleObjects]
    public class SwapPrefabManagerEditor : Editor
    {
        public string category;
        public string prefab;
        public string variation;

        SwapPrefab _target;
        public int categoryIndex;
        public int prefabIndex;
        public int variationIndex;
        public int exCategoryIndex;
        public int exPrefabIndex;
        public int exVariationIndex;


        private void OnEnable()
        {
            _target = (SwapPrefab)target;

            if (!_target.placed)
            {
                if (!PrefabUtility.IsPartOfPrefabAsset(_target.gameObject) && PrefabStageUtility.GetPrefabStage(_target.gameObject) == null)
                {
                    _target.placed = true;
                    SpawnAlongs();
                    EditorUtility.SetDirty(_target);
                }
            }

            _target.FindPrefabProperties();

            _target.CreateCategoryList();
            categoryIndex = SwapPrefab.categoryList.IndexOf(_target.category);
            exCategoryIndex = categoryIndex;

            _target.CreatePrefabList();
            prefabIndex = SwapPrefab.prefabList.IndexOf(_target.prefab);
            exPrefabIndex = prefabIndex;

            _target.CreateVariationList();
            variationIndex = SwapPrefab.variationList.IndexOf(_target.variation);
            exVariationIndex = variationIndex;
        }

        public override void OnInspectorGUI()
        {
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, wordWrap = true };

            if (!_target)
                return;
            else if (!_target.gameObject.activeInHierarchy)
                return;

            if (PrefabStageUtility.GetPrefabStage(_target.gameObject) != null)
                return;

            if (_target.transform.parent != null && PrefabUtility.GetOutermostPrefabInstanceRoot(_target.transform) != null)
                if (PrefabUtility.GetPrefabInstanceStatus(_target.transform.parent) == PrefabInstanceStatus.Connected)
                    if (PrefabUtility.GetOutermostPrefabInstanceRoot(_target.transform).gameObject == _target.transform.parent.gameObject)
                        EditorGUILayout.LabelField("!!! Nested prefab !!!" + "\n" + "Swapping this prefab will break the parent prefab connection", labelStyle);

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            categoryIndex = EditorGUILayout.Popup("Category: ", categoryIndex, SwapPrefab.categoryList.ToArray(), EditorStyles.popup);
            prefabIndex = EditorGUILayout.Popup("Prefab: ", prefabIndex, SwapPrefab.prefabList.ToArray(), EditorStyles.popup);
            variationIndex = EditorGUILayout.Popup("Variation: ", variationIndex, SwapPrefab.variationList.ToArray(), EditorStyles.popup);

            if (exVariationIndex != variationIndex)
            {
                //Debug.Log("variation changed");
                SwapToPrefab(SwapPrefab.variationList[variationIndex]);
            }

            if (exPrefabIndex != prefabIndex)
            {
                //Debug.Log("prefab changed");
                SwapToPrefab(SwapPrefab.prefabList[prefabIndex]);
            }

            if (exCategoryIndex != categoryIndex)
            {
                //Debug.Log("category changed");
                _target.category = SwapPrefab.categoryList[categoryIndex];
                _target.CreatePrefabList();
                _target.prefab = SwapPrefab.prefabList[0];
                prefabIndex = -1;
                _target.CreateVariationList();
                _target.variation = SwapPrefab.variationList[0];
                variationIndex = 0;
            }


            exCategoryIndex = categoryIndex;
            exPrefabIndex = prefabIndex;
            exVariationIndex = variationIndex;


            if (_target.quickSwaps != null)
            {
                if (_target.quickSwaps.Length > 0)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Quickswap", labelStyle);

                    for (int i = 0; i < _target.quickSwaps.Length; i++)
                        if (_target.quickSwapsBtns.Length >= i + 1)
                        {
                            if (GUILayout.Button(_target.quickSwapsBtns[i]))
                                SwapToPrefab(_target.quickSwaps[i]);
                        }
                        else
                        {
                            if (GUILayout.Button(_target.quickSwaps[i]))
                                SwapToPrefab(_target.quickSwaps[i]);
                        }
                }
            }

            if (_target.additionals != null)
            {
                if (_target.additionals.Length > 0)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Spawn additional", labelStyle);
                    Vector3 additionalPos = Vector3.zero;
                    Vector3 additionalRot = Vector3.zero;

                    for (int i = 0; i < _target.additionals.Length; i++)
                    {
                        if (_target.additionalsPos.Length >= i + 1)
                            if (_target.additionalsPos[i] != null)
                                additionalPos = _target.additionalsPos[i];
                        if (_target.additionalsRot.Length >= i + 1)
                            if (_target.additionalsRot[i] != null)
                                additionalRot = _target.additionalsRot[i];

                        if (_target.additionalsBtns.Length >= i + 1)
                        {
                            if (GUILayout.Button(_target.additionalsBtns[i]))
                                SpawnAdditionalPrefab(_target.additionals[i], additionalPos, additionalRot);
                        }
                        else
                        {
                            if (GUILayout.Button(_target.additionals[i]))
                                SpawnAdditionalPrefab(_target.additionals[i], additionalPos, additionalRot);
                        }
                    }
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Revert prefab", labelStyle);
            if (GUILayout.Button("Revert Prefab"))
                SwapToPrefab(_target.prefab);
        }

        public void SwapToPrefab(string name)
        {
            if (_target.transform.parent != null && PrefabUtility.GetOutermostPrefabInstanceRoot(_target.transform) != null)
                if (PrefabUtility.GetPrefabInstanceStatus(_target.transform.parent) == PrefabInstanceStatus.Connected)
                    if (PrefabUtility.GetOutermostPrefabInstanceRoot(_target.transform).gameObject == _target.transform.parent.gameObject)
                        PrefabUtility.UnpackPrefabInstance(_target.transform.parent.gameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

            GameObject swapped = (GameObject)PrefabUtility.InstantiatePrefab(FindSwapPrefab(name));
            swapped.transform.position = _target.transform.position;
            swapped.transform.rotation = _target.transform.rotation;
            swapped.transform.parent = _target.transform.parent;
            swapped.name = name;

            swapped.GetComponent<SwapPrefab>().Initialize();
            Selection.activeGameObject = swapped;
            DestroyImmediate(_target.gameObject);

        }

        public void SpawnAdditionalPrefab(string name, Vector3 posOffset, Vector3 rotOffset)
        {
            GameObject added = (GameObject)PrefabUtility.InstantiatePrefab(FindPrefab(name));
            added.transform.position = _target.transform.position;
            added.transform.rotation = _target.transform.rotation;
            added.transform.parent = _target.transform;
            added.name = name;

            added.transform.Translate(posOffset);
            added.transform.Rotate(rotOffset);
            _target.Initialize();
        }

        public UnityEngine.Object FindSwapPrefab(string name)
        {
            string[] files;
            foreach (string path in SwapPrefabList.prefabsFolders)
            {
                files = System.IO.Directory.GetFiles(path, "*.prefab", System.IO.SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    GameObject asset = (GameObject)AssetDatabase.LoadMainAssetAtPath(file);
                    if (asset.GetComponent<SwapPrefab>() != null && asset.name == name)
                        return asset;
                }
            }

            Debug.LogWarning("Prefab not found ! " + name);
            return null;
        }

        public void SpawnAlongs()
        {
            //if (spawnAlongs.Length > 0)
            for (int i = 0; i < _target.spawnAlongs.Length; i++)
            {
                Vector3 spawnPos = Vector3.zero;
                Vector3 spawnRot = Vector3.zero;

                if (_target.spawnAlongsPos.Length >= i + 1)
                    spawnPos = _target.spawnAlongsPos[i];
                if (_target.spawnAlongsRot.Length >= i + 1)
                    spawnRot = _target.spawnAlongsRot[i];

                SpawnAdditionalPrefab(_target.spawnAlongs[i], spawnPos, spawnRot);
            }
        }

        public UnityEngine.Object FindPrefab(string name)
        {
            string[] files;
            foreach (string path in SwapPrefabList.prefabsFolders)
            {
                files = System.IO.Directory.GetFiles(path, "*.prefab", System.IO.SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    GameObject asset = (GameObject)AssetDatabase.LoadMainAssetAtPath(file);
                    if (asset.name == name)
                        return asset;
                }
            }

            Debug.LogWarning("Prefab not found ! " + name);
            return null;
        }

        void PrintAllSwapPrefabs()
        {
            string[] assetsPaths = AssetDatabase.GetAllAssetPaths();
            int assetsFound = 0;
            foreach (string assetpath in assetsPaths)
            {
                if (AssetDatabase.GetMainAssetTypeAtPath(assetpath) == typeof(GameObject))
                {
                    GameObject asset = (GameObject)AssetDatabase.LoadAssetAtPath(assetpath, typeof(GameObject));
                    if (asset.GetComponent<SwapPrefab>() != null)
                    {
                        Debug.Log("Found Swap Prefab:" + assetpath);
                        assetsFound++;
                    }
                }
            }
        }
    }
}

//*****  (©) Finward Studios 2021. All rights reserved. *****\\