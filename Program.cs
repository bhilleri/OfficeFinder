using System.IO.Enumeration;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

Console.WriteLine("test");


using (WordprocessingDocument wordDoc = WordprocessingDocument.Open("test.docx", false)){
    MainDocumentPart mainDocument= wordDoc.MainDocumentPart;
    Body body = mainDocument.Document.Body;
    string text = body.InnerText;

    string pattern = @"test";
    RegexOptions options = RegexOptions.Multiline;
    
    foreach (Match m in Regex.Matches(text, pattern, options))
    {
        Console.WriteLine("'{0}' found at index {1}.", m.Value, m.Index);
    }
}
