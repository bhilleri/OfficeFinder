using System.IO.Enumeration;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

// Permet pour le moment de rechercher une regex dans un fichier
// Taper dotnet run ./OfficeFinder <pattern> <File>
string pattern = Environment.GetCommandLineArgs()[2];
string fileName = Environment.GetCommandLineArgs()[3];


using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileName, false)){
    MainDocumentPart mainDocument= wordDoc.MainDocumentPart;
    Body body = mainDocument.Document.Body;
    string text = body.InnerText;

    
    RegexOptions options = RegexOptions.Multiline;
    
    foreach (Match m in Regex.Matches(text, pattern, options))
    {
        Console.WriteLine("'{0}' found at index {1}.", m.Value, m.Index);
    }
}
