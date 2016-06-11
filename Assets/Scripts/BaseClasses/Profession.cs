using System.ComponentModel;

[System.Serializable]
public enum Profession {
    None = 0,
    Oceanographer = 1,
    Reconnaissance = 2,
    [Description("Bird Watcher")]
    BirdWatcher = 3,
}


