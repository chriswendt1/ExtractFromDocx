using System.Diagnostics;

Console.WriteLine("Extract text from parallel text documents in TSV table format");

if (args.Length > 0)
{
    string rootPath = args[0];
    const string outFileName = "collection.tsv";
    string outFile = rootPath + Path.DirectorySeparatorChar + outFileName;
    File.Delete(outFile);
    TraverseDirectory(rootPath, outFile, ProcessFile);
    return 0;
}
else
    Console.WriteLine("ERROR: Please provide a folder path as argument.");
return 1;

void TraverseDirectory(string path, string outFile, Action<string, string> action)
{
    foreach (string file in Directory.GetFiles(path))
    {
        if ((file.ToUpperInvariant().Contains("_EN.") || file.Contains("Source")) && (Path.GetExtension(file).ToLowerInvariant() == ".txt"))
        {
            action(file, outFile);
            continue;
        }
    }

    foreach (string directory in Directory.GetDirectories(path))
    {
        TraverseDirectory(directory, outFile, action);
    }
}

void ProcessFile(string txtFileName, string outFileName)
{
    Debug.WriteLine(txtFileName);
    Console.Write(".");
    List<string> textsSource = new();
    
    textsSource = File.ReadAllLines(txtFileName, System.Text.Encoding.UTF8).ToList();


    string targetFileName = txtFileName.Replace("_EN.", "_ES.");
    if ((!File.Exists(targetFileName)) || (targetFileName == txtFileName))
        targetFileName = txtFileName.Replace("Source", "Translated");
    if ((!File.Exists(targetFileName)) || (targetFileName == txtFileName))
    {
        Console.WriteLine($"ERROR: Target file {targetFileName} not found.");
        return;
    }
    List<string> textsTarget = new();
    textsTarget = File.ReadAllLines(targetFileName, System.Text.Encoding.UTF8).ToList();

    StreamWriter outFile = File.AppendText(outFileName);

    List<TextPair> pairs = TextPairs.Align(textsSource, textsTarget);
    foreach (TextPair pair in pairs)
    {
        outFile.WriteLine(pair.sourceText + "\t" + pair.targetText);
    }
    outFile.Close();
}
