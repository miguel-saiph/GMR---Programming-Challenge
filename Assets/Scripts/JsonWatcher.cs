using System.IO;
using UnityEngine;

public class JsonWatcher : MonoBehaviour
{
    private string path;
    [SerializeField] private string file;
    private bool changed;
    private FileSystemWatcher watcher;
    private DynamicTableGenerator dynamicTableGenerator;

    private void OnEnable()
    {
        dynamicTableGenerator = Object.FindObjectOfType<DynamicTableGenerator>();

        path = Application.dataPath + "/StreamingAssets";
        if (!File.Exists(Path.Combine(path, file)))
        {
            return;
        }

        watcher = new FileSystemWatcher();
        watcher.Path = path;
        watcher.Filter = file;

        // Watch for changes in LastAccess and LastWrite times, and
        // the renaming of files or directories.
        watcher.NotifyFilter = NotifyFilters.LastWrite;

        // Add event handlers
        watcher.Changed += OnChanged;

        // Begin watching
        watcher.EnableRaisingEvents = true;
    }

    private void OnDisable()
    {
        if(watcher != null)
        {
            watcher.Changed -= OnChanged;
            watcher.Dispose();
        }
    }


    private void Update()
    {
        if (changed)
        {
            dynamicTableGenerator.CreateTable();
            changed = false;
        }
    }


    private void OnChanged(object source, FileSystemEventArgs e)
    {
        changed = true;
    }
    
}

// SRC
// https://stackoverflow.com/questions/56672064/unity-with-filesystemwatcher