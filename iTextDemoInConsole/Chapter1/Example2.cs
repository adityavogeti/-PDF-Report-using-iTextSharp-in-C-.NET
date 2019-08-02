using iTextSharp.text.html;	
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;


namespace iTextSharp.tutorial.Chapter1
{
	/// <summary>
	/// Example 2 shows the Following:
	/// 1. Working with Page Size
	/// 2. Setting Background Color of PDF Document
	/// </summary>
	public class Example2
	{
		public Example2()
		{
			 //Creating a Page of specified size, we must have to create a iTextSharp.text.Rectangle object
			 //and Passing the size as argument to its constructor

			 //1. First Way to define Page Size
			 //Creating Page Size by Pixels or Inch
			 //NOTE: In iTextSharp library, unit is 'point'. 72 points = 1 inch
			 //Creating a PDF File of width = 2 inch & height = 10 inch
			 //So, I've to provide 144pt for 2 inch & 72pt for 10 inch
			Rectangle rec = new Rectangle(144, 720);

			 //2. Second Way to define Page Size
			 //Taking Page Size from in-built iTextSharp.text.PageSize class
			Rectangle rec2 = new Rectangle(PageSize.A4);

			 //3. Third Way to define Page Size: Rotating Document i.e. height becomes width & vice-versa
			Rectangle rec3 = new Rectangle(PageSize.A4.Rotate());

			
			// Setting Background Color of PDF Document
			
			// First Way to Set Background Color
			// It takes the object of iTextSharp.text.BaseColor
			// BaseColor constructor takes in-built System.Drawing.Color object
			// Or you can pass RGB values to the constructor in different forms.
			rec.BackgroundColor = new BaseColor(System.Drawing.Color.WhiteSmoke);

			// Second Way to Set Background Color
			// It takes the object of  iTextSharp.text.pdf.CMYKColor
			// CMYKColor constructor takes only CMYK values in different forms
			rec2.BackgroundColor = new CMYKColor(25, 90, 25, 0);

			string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
			try
			{
				// Step 1: Creating System.IO.FileStream object
				using (FileStream fs = new FileStream(appRootDir + "/PDFs/" + "Chapter1_Example2.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
				// Step 2: Creating iTextSharp.text.Document object
				// Passing iTextSharp.text.Rectangle object 'rec' what I've just created to the Document object
				// For the time being, I'm just creting PDF Document from 'rec', not using 'rec2' & 'rec3'
				using (Document doc = new Document(rec))
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
