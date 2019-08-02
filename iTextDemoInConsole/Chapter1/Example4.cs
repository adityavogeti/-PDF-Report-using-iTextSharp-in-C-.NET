using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iTextSharp.tutorial.Chapter1
{
	/// <summary>
	/// Example 4 shows: Setting Meta Information or PDF Document Properties
	/// </summary>
	public class Example4
	{
		public Example4()
		{
			string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
			try
			{
				// Step 1: Creating System.IO.FileStream object
				using (FileStream fs = new FileStream(appRootDir + "/PDFs/" + "Chapter1_Example4.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
				// Step 2: Creating iTextSharp.text.Document object
				using (Document doc = new Document())
				// Step 3: Creating iTextSharp.text.pdf.PdfWriter object
				// It helps to write the Document to the Specified FileStream
				using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
				{
					// Setting Document properties e.g.
					// 1. Title
					// 2. Subject
					// 3. Keywords
					// 4. Creator
					// 5. Author
					// 6. Header
					doc.AddTitle("Hello World example");
					doc.AddSubject("This is an Example 4 of Chapter 1 of Book 'iText in Action'");
					doc.AddKeywords("Metadata, iTextSharp 5.4.4, Chapter 1, Tutorial");
					doc.AddCreator("iTextSharp 5.4.4");
					doc.AddAuthor("Debopam Pal");
					doc.AddHeader("Nothing", "No Header");

					// Step 4: Openning the Document
					doc.Open();

					// Step 5: Adding a paragraph
					// NOTE: When we want to insert text, then we've to do it through creating paragraph
					doc.Add(new Paragraph("Hello World"));

					// Step 6: Closing the Document
					doc.Close();
				}
			}
			// Catching iTextSharp.text.DocumentException if any
			catch (DocumentException de)
			{
				throw de;
			}
			// Catching System.IO.IOException if any
			catch (IOException ioe)
			{
				throw ioe;
			}
		}
	}
}
