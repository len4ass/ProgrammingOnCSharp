using System;
using System.Collections.Generic;
using System.Linq;

internal sealed partial class Folder
{
    internal class Backup
    {
        private readonly List<File> _files = new List<File>();

        public Backup(Folder folder)
        {
            foreach (var file in folder.files)
            {
                _files.Add(file);
            }
        }
        
        public static void Restore(Folder folder, Backup backup)
        {
            folder.files = backup._files;
        }
    }

    public void AddFile(string name, int size)
    {
        files.Add(new File(name, size));
    }

    public void RemoveFile(File file)
    {
        files.Remove(file);
    }

    public File this[int i]
    {
        get 
        {
            if (i < 0 || i >= files.Count)
            {
                throw new IndexOutOfRangeException("Not enough files or index less zero");
            }

            return files[i];
        }
    }

    public File this[string filename]
    {
        get
        {
            foreach (var file in files.Where(file => file.Name == filename))
            {
                return file;
            }

            throw new ArgumentException("File not found");
        }
    }

    public override string ToString()
    {
        return $"Files in folder:\n{string.Join("\n", files)}";
    }
}