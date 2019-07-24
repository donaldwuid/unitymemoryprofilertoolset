using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Assets.Editor.Treemap;
using Treemap;
using UnityEditor;
using UnityEngine;
using System;
using System.Net;
using NUnit.Framework.Constraints;
using UnityEditor.MemoryProfiler;
using Object = UnityEngine.Object;
using System.IO;

namespace MemorySnapshotConvertor
{
    public class MemorySnapshotConvertorWindow : EditorWindow
    {
        private UnityEditor.MemoryProfiler.PackedMemorySnapshot _snapshot;
        private bool _registered = false;
        private string previousDirectory = null;
        
        private void TryInitialize()
        {
            if (!_registered)
            {
                UnityEditor.MemoryProfiler.MemorySnapshot.OnSnapshotReceived += IncomingSnapshot;
                _registered = true;
            }
        }
        void OnGUI()
        {
            TryInitialize();

            if (GUILayout.Button("Take Snapshot"))
            {
                UnityEditor.EditorUtility.DisplayProgressBar("Take Snapshot", "Downloading Snapshot...", 0.0f);
                try
                {
                    UnityEditor.MemoryProfiler.MemorySnapshot.RequestNewSnapshot();
                }
                finally
                {
                    EditorUtility.ClearProgressBar();
                }
            }

            if (null != _snapshot)
            {
                GUILayout.Label("Captured: SnapshotHash=" + _snapshot.GetHashCode());
            }
            else
            {
                GUILayout.Label("Captured: None");
            }

            //EditorGUI.BeginDisabledGroup(_snapshot == null);
            //EditorGUI.EndDisabledGroup();
            if (null != _snapshot)
            {
                if (GUILayout.Button("Save Snapshot..."))
                {
                    SaveToFile(_snapshot);
                }
                if (GUILayout.Button("Clear Snapshot"))
                {
                    _snapshot = null;
                }
            }
            else
            {
            }
        }
        public void SaveToFile(PackedMemorySnapshot snapshot)
        {
            var filePath = EditorUtility.SaveFilePanel("Save Snapshot", previousDirectory, "MemorySnapshot", "snap");
            if(string.IsNullOrEmpty(filePath))
                return;

            previousDirectory = Path.GetDirectoryName(filePath);
            MemorySnapshotConvertorWriter writer = new MemorySnapshotConvertorWriter();
            writer.WriteToFile(filePath, _snapshot);
        }
        void IncomingSnapshot(PackedMemorySnapshot snapshot)
        {
            _snapshot = snapshot;
        }
    }
}
