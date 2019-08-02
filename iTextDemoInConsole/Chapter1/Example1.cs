using System;
using System.IO;
// Importing necessary Library to work with iTextSharp 5.4.4
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iTextSharp.tutorial.Chapter1
{
	/// <summary>
	/// Exmaple 1: Creation of a PDF Document in 6 steps
	/// </summary>
	public class Example1
	{
		public Example1()
		{
			string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
			try
			{
				// Step 1: Creating System.IO.FileStream object
				using (FileStream fs = new FileStream(appRootDir + "/PDFs/" + "Chapter1_Example1.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
				// Step 2: Creating iTextSharp.text.Document object
				using (Document doc = new Document())
				// Step 3: Creating iTextSharp.text.pdf.PdfWriter object
				// It helps to write the Document to the Specified FileStream
				using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
				{
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
