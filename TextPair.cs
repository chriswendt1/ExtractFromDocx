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

        for (int i = 0; i < Math.Max(sourceTexts.Count, targetTexts.Count); i++)
        {
            TextPair pair = new();
            if (i >= sourceTexts.Count)
            {
                pair.sourceText = null;
                pair.targetText = targetTexts[i].Trim();
            }
            else if (i >= targetTexts.Count)
            {
                pair.sourceText = sourceTexts[i].Trim();
                pair.targetText = null;
            }
            else
            {
                pair.sourceText = sourceTexts[i].Trim();
                pair.targetText = targetTexts[i].Trim();
            }
            if (pair.sourceText is null && pair.targetText is null) continue;
            pairs.Add(pair);
        }
        return pairs;
    }
}
