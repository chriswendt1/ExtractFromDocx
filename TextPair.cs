using System.Text.RegularExpressions;

struct TextPair
{
    public string? sourceText;
    public string? targetText;
}

static partial class TextPairs
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
                sourceText = i < newSourceTexts.Count ? RemoveMarkup(newSourceTexts[i]) : null,
                targetText = i < newTargetTexts.Count ? RemoveMarkup(newTargetTexts[i]) : null
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
            if (!string.IsNullOrEmpty(input.Trim())) result.Add(input.Trim());
        }
        return result;
    }

    private static string RemoveMarkup(string tuv)
    {
        /*
        // regular expression pattern to match XML tags
        string pattern = @"<[^>]+>";

        // remove all XML tags from the string
        string plainText = Regex.Replace(tuv, pattern, "");
        */
        //other cleanup
        string plainText = tuv;

        plainText = plainText.Replace("\t", " ");
        plainText = plainText.Replace("•", "");
        plainText = plainText.StartsWith("-") ? plainText[1..] : plainText;
        plainText = plainText.StartsWith("■") ? plainText[1..] : plainText;
        plainText = plainText.StartsWith("*") ? plainText[1..] : plainText;
        plainText = plainText.StartsWith("o ") ? plainText[2..] : plainText;
        plainText = plainText.StartsWith("\"") ? "\"" + plainText : plainText;
        plainText = RxMultiPeriod().Replace(plainText, ".");


        // output plain text string
        return plainText.Trim();

    }

    [GeneratedRegex("\\.+")]
    private static partial Regex RxMultiPeriod();
}


