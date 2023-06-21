using System;
using System.Collections;
using System.Data;
using System.Text;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using DoiTLean.PDFHelper.Structures;

namespace DoiTLean.PDFHelper
{
    /// <summary>
    /// This extension contains very commonly used actions for PDF
    /// </summary>
    public class PDFHelper : IPDFHelper {

        private const string PDFHelperPrefix = "PDFHelper_";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PDF"></param>
        /// <param name="Images"></param>
        /// <param name="ErrorMessage"></param>
        public void ExtractImagesFromPDF(byte[] PDF, out List<ImageRecord> Images, out string ErrorMessage)
        {
            Images = new List<ImageRecord>();
            ErrorMessage = "";


            //Create temporary in File in the filesystem
            string inFilePath = Path.GetTempPath() + GetPDFTempFilename();
            File.WriteAllBytes(inFilePath, PDF);

            string outFilePath = CreateUniqueTempDirectory();


            ImageExtractor.ExtractImagesFromFile(inFilePath, PDFHelperPrefix, outFilePath, true);

            foreach (string file in Directory.GetFiles(outFilePath))
            {
                byte[] fileByte = File.ReadAllBytes(file);
                string ext = Path.GetExtension(file);

                ImageRecord imgRecord = new ImageRecord(fileByte, ext);
                Images.Add(imgRecord);

            }

        } // ExtractImagesFromPDF




        /// <summary>
        /// 
        /// </summary>
        /// <param name="PDF">Binary data of the PDF</param>
        /// <param name="OutPDF">PDF Binary Data with JS removed.</param>
        /// <param name="ErrorMessage"></param>
        public void RemoveJavascript(byte[] PDF, out byte[] OutPDF, out string ErrorMessage)
        {
            OutPDF = new byte[] { };
            ErrorMessage = "";

            try
            {
                using (MemoryStream inputStream = new MemoryStream(PDF))
                {
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        PdfDocument pdfDoc = new PdfDocument(new PdfReader(inputStream), new PdfWriter(outputStream));

                        for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                        {
                            PdfPage page = pdfDoc.GetPage(i);
                            PdfDictionary pageDict = page.GetPdfObject();

                            if (pageDict.ContainsKey(PdfName.JavaScript))
                            {
                                pageDict.Remove(PdfName.JavaScript);
                            }

                            if (pageDict.ContainsKey(PdfName.JS))
                            {
                                pageDict.Remove(PdfName.JS);
                            }

                            if (pageDict.ContainsKey(PdfName.AA))
                            {
                                pageDict.Remove(PdfName.AA);
                            }
                        }

                        pdfDoc.Close();

                        // Read the generated file in bytes
                        OutPDF = outputStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message + " " + ex.StackTrace;
            }
        } // RemoveJavascript



        private string GetPDFTempFilename()
        {
            return PDFHelperPrefix + Guid.NewGuid().ToString() + ".pdf";
        }





        /// <summary>
        /// Creates the unique temporary directory.
        /// </summary>
        /// <returns>
        /// Directory path.
        /// </returns>
        public string CreateUniqueTempDirectory()
        {
            var uniqueTempDir = Path.GetFullPath(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));
            Directory.CreateDirectory(uniqueTempDir);
            return uniqueTempDir;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="PDF"></param>
        /// <param name="Arguments"></param>
        /// <param name="CompressedPDF"></param>
        /// <param name="isSuccess"></param>
        /// <param name="Message"></param>
        public void CompressPDF(byte[] PDF, List<Argument> Arguments, out byte[] CompressedPDF, out bool isSuccess, out string Message)
        {
            CompressedPDF = new byte[] { };
            isSuccess = false;
            Message = "";

            //Create temporary in File in the filesystem
            string inFilePath = Path.GetTempPath() + GetPDFTempFilename();
            File.WriteAllBytes(inFilePath, PDF);

            string outFilePath = Path.GetTempPath() + GetPDFTempFilename();
          

        } // CompressPDF


        /// <summary>
        /// 
        /// </summary>
        /// <param name="PDFBytes"></param>
        /// <param name="Comments"></param>
		/// <param name="ErrorMessage"></param>
        public void GetPDFAnnotation(byte[] PDFBytes, out string ErrorMessage, out List<PDFComment> Comments)
        {
            Comments = new List<PDFComment>();
            ErrorMessage = "";
          
        } // GetPDFAnnotation


        /// <summary>
        /// 
        /// </summary>
        /// <param name="PDF"></param>
        /// <param name="Watermark_Text"></param>
        /// <param name="Text_Size"></param>
        /// <param name="OutPDF"></param>
        /// <param name="ErrorMessage"></param>
        public void AddTextWatermarkInPDF_Memory(byte[] PDF, string Watermark_Text, int Text_Size, out byte[] OutPDF, out string ErrorMessage)
        {
            OutPDF = new byte[] { };
            ErrorMessage = "";
         
        } // AddTextWatermarkInPDF_Memory



        /// <summary>
        /// 
        /// </summary>
        /// <param name="PDFBytes"></param>
        /// <param name="PDFText"></param>
        /// <param name="ErrorMessage"></param>
        public void ReadTextFromPDF(byte[] PDFBytes, out string PDFText, out string ErrorMessage)
        {
            PDFText = ""; ErrorMessage = "";
           

        } // ReadTextFromPDF

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PDF">Binary Data of the pdf file.</param>
        /// <param name="Watermark_Text">Text Needs to be printed as water mark.</param>
        /// <param name="Text_Size">Size of Text</param>
        /// <param name="Temp_Path">Path at which the new watermarked PDF will be generated.</param>
        /// <param name="OutPDF">PDF Bytes with water mark text.</param>
        /// <param name="ErrorMessage"></param>
        public void AddTextWatermarkInPDF_TempPath(byte[] PDF, string Watermark_Text, int Text_Size, string Temp_Path, out byte[] OutPDF, out string ErrorMessage)
        {
            OutPDF = new byte[] { };
            ErrorMessage = "";
          
        } // AddTextWatermarkInPDF

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PdfByte">PDF Filedata</param>
        /// <param name="XAxis"></param>
        /// <param name="YAxis"></param>
        /// <param name="PaginationText"></param>
        /// <param name="ErrorMsg"></param>
        /// <param name="PdfOutByte"></param>
        public void PrintPageNumber(byte[] PdfByte, int XAxis, int YAxis, string PaginationText, out string ErrorMsg, out byte[] PdfOutByte)
        {
            ErrorMsg = "";
            PdfOutByte = new byte[] { };

            if (String.IsNullOrEmpty(PaginationText))
            {
                PaginationText = "Page {page_number} of {total_pages}";
            }

        } // PrintPageNumber

        /// <summary>
        /// Join Multiple PDF  files in single file
        /// </summary>
        /// <param name="PDFFileList"></param>
        /// <param name="JoinedFile"></param>
        /// <param name="ErrorMessage"></param>
        public void JoinPDF(List<PDFFile> PDFFileList, out byte[] JoinedFile, out string ErrorMessage)
        {
            JoinedFile = new byte[] { };
            ErrorMessage = "";
            int i = 0;
           
        } // JoinPDF


        public static byte[] JoinFiles(List<byte[]> sourceFiles)
        {
            return null;
        }


    }
}
