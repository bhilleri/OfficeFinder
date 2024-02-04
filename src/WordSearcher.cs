namespace OfficeFinder;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

/// <summary>
/// <inheritdoc cref="IWordSearcher"/>
/// </summary>
public class WordSearcher : IWordSearcher
{
    private string filePath;
    public WordSearcher(string filePath){
        this.filePath = filePath;
    }
    public KeyValuePair<string, int> Search(string regexPattern)
    {
        int count = 0;
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
        {

            if (wordDoc is null)
            {
                throw new ArgumentNullException(nameof(wordDoc));
            }

            MainDocumentPart mainDocument = wordDoc.MainDocumentPart ?? wordDoc.AddMainDocumentPart();
            Body body = mainDocument.Document.Body ?? new Body();

            RegexOptions options = RegexOptions.Multiline| RegexOptions.IgnoreCase;
            OpenXmlElementList elements = body.ChildElements;
            string innerText = "";
            foreach(OpenXmlElement element in elements){
                innerText += element.InnerText + "\n";  // To separe differents paragraph
            }
            MatchCollection AllMatchs = Regex.Matches(innerText, regexPattern, options);
            count = AllMatchs.Count;
        }
        return new KeyValuePair<string, int>(this.filePath, count);
    }
}