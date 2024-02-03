using System.IO.Enumeration;
using DocumentFormat.OpenXml.Packaging;

using (WordprocessingDocument wordDoc = WordprocessingDocument.Open("test.docx", false)){
    Console.WriteLine("Hello world");
}