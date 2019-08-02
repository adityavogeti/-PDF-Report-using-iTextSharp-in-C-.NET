using System;
using System.IO;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace iTextSharp.tutorial.Chapter1
{
	/// <summary>
	/// Example 3 shows the Following:
	/// 1. Setting with Page Margins
	/// 2. Setting with the Text/Paragraph Alignment
	/// </summary>
	public class Example3
	{
		public Example3()
		{
			Document document = new Document(PageSize.A5, 36, 72, 108, 180);
			string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
			try
			{
				// Step 1: Creating System.IO.FileStream object
				using (FileStream fs = new FileStream(appRootDir + "/PDFs/" + "Chapter1_Example3.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
				// Step 2: Creating iTextSharp.text.Document object
				// Adding PageSize and Margins during Document creation
				// NOTE: In iTextSharp library, 72 points = 1 inch
				// Left Margin:		36pt	=> 0.5 inch
				// Right Margin:	72pt	=> 1 inch
				// Top Margin:		108pt	=> 1.5 inch
				// Bottom Margini:	180pt	=> 2.5 inch
				using (Document doc = new Document(PageSize.A4, 36, 72, 108, 180))
				// Step 3: Creating iTextSharp.text.pdf.PdfWriter object
				// It helps to write the Document to the Specified FileStream
				using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
				{
					// Step 4: Openning the Document
					doc.Open();

					// Step 5: Adding a paragraph
					// NOTE: When we want to insert text, then we've to do it through creating paragraph
					// Creating iTextSharp.text.Paragraph object
					Paragraph para = new Paragraph("Hello World Hello World Hello World Hello World Hello World Hello World Hello World Hello World Hello World Hello World Hello World");
					// Setting paragraph's text alignment using iTextSharp.text.Element class
					para.Alignment = Element.ALIGN_JUSTIFIED;
					// Adding this 'para' to the Document object
					doc.Add(para);

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
