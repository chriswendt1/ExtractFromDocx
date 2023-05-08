using System.Runtime.CompilerServices;

struct TextPair
{
    public string? sourceText;
    public string? targetText;
}

static class TextPairs
{
    public static List<TextPair> Align(List<string> sourceTexts, List<string> targetTexts)
    {
        List<TextPair> pairs = new();
        List<string> newSourceTexts = new();
        List<string> newTargetTexts = new();
        newSourceTexts.AddRange(RemoveEmpty(sourceTexts));
        newTargetTexts.AddRange(RemoveEmpty(targetTexts));
        int count = Math.Max(newSourceTexts.Count, newTargetTexts.Count);
        for (int i = 0; i < count; i++)
        {
            TextPair pair = new()
            {
                sourceText = i < newSourceTexts.Count ? newSourceTexts[i].Trim() : null,
                targetText = i < newTargetTexts.Count ? newTargetTexts[i].Trim() : null
            };
            pairs.Add(pair);
        }
        return pairs;
    }

    private static List<string> RemoveEmpty(List<string> inputList)
    {
        List<string> result = new();
        foreach (string input in inputList)
        {
            if (!(string.IsNullOrEmpty(input.Trim()))) result.Add(input.Trim());
        }
        return result;
    }
}


