using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Xobject;
using System;
using System.Collections.Generic;
using System.IO;

namespace DoiTLean.PDFHelper
{
    /// <summary>
    /// Wrapper class for iText7 to easily dump all images from a PDF into separate files in a folder.
    /// </summary>
    /// <remarks>
    /// Adapted from article at:
    /// http://www.thevalvepage.com/swmonkey/2014/11/26/extract-images-from-pdf-files-using-itextsharp/
    /// </remarks>
    public class ImageExtractor : IEventListener
    {
        /// <summary>
        /// Current page in document.
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// Current count of images for entire document up to this point.
        /// </summary>
        public int ImageCount { get; private set; }

        private readonly string _OutputFilePrefix;
        private readonly string _OutputPath;
        private readonly bool _OverwriteExistingFiles;

        /// <summary>
        /// Helper class to dump all images from a PDF into separate files.
        /// </summary>
        /// <param name="outputFilePrefix">Prefix to use for files created. Uses source file name if null.</param>
        /// <param name="outputPath">Output files will be written to this path.</param>
        /// <param name="overwriteExistingFiles">Should existing files be overwritten?</param>
        private ImageExtractor(string outputFilePrefix, string outputPath, bool overwriteExistingFiles)
        {
            CurrentPage = 1;
            ImageCount = 0;

            _OutputFilePrefix = outputFilePrefix;
            _OutputPath = outputPath;
            _OverwriteExistingFiles = overwriteExistingFiles;
        }

        /// <summary>
        /// Extract all images from a PDF file. Images will be extracted in their native format.
        /// </summary>
        /// <param name="pdfPath">Full path and file name of PDF file</param>
        /// <param name="outputFilePrefix">
        /// Basic name of exported files. If null then uses same name as PDF file.
        /// </param>
        /// <param name="outputPath">
        /// Images will be saved to this path. If null or empty then uses same folder as PDF file.
        /// </param>
        /// <param name="overwriteExistingFiles">
        /// True to overwrite existing image files, false to skip past them
        /// </param>
        /// <returns>
        /// Count of total number of images extracted.
        /// </returns>
        public static int ExtractImagesFromFile(
            string pdfPath,
            string outputFilePrefix,
            string outputPath,
            bool overwriteExistingFiles)
        {
            // Handle setting of default values
            outputFilePrefix = outputFilePrefix ?? Path.GetFileNameWithoutExtension(pdfPath);
            outputPath = String.IsNullOrEmpty(outputPath) ? Path.GetDirectoryName(pdfPath) : outputPath;

            var extractor = new ImageExtractor(outputFilePrefix, outputPath, overwriteExistingFiles);

            using (var pdfReader = new PdfReader(pdfPath))
            {
                //Skip encrypted PDFs for now
                if (!pdfReader.IsEncrypted())
                {
                    var pdfDocument = new PdfDocument(pdfReader);
                    var pdfParser = new PdfCanvasProcessor(extractor);
                    while (extractor.CurrentPage <= pdfDocument.GetNumberOfPages())
                    {
                        pdfParser.ProcessPageContent(pdfDocument.GetPage(extractor.CurrentPage));
                        extractor.CurrentPage++;
                    }
                }
            }

            return extractor.ImageCount;
        }

        #region Implementation of IEventListener

        public void EventOccurred(IEventData data, EventType type)
        {
            if (type == EventType.RENDER_IMAGE)
            {
                var renderInfo = (ImageRenderInfo)data;
                var imageObject = renderInfo.GetImage();
                var imageXObject = (PdfImageXObject)imageObject;

                var imageFileType = "";
                var subtypeObj = imageXObject.GetPdfObject().GetAsName(PdfName.Subtype);
                if (subtypeObj != null)
                {
                    var subtype = subtypeObj.GetValue();
                    imageFileType = subtype.ToLower();
                }

                var imageFileName = String.Format("{0}_{1}_{2}.{3}", _OutputFilePrefix, CurrentPage, ImageCount, imageFileType);
                var imagePath = Path.Combine(_OutputPath, imageFileName);

                if (_OverwriteExistingFiles || !File.Exists(imagePath))
                {
                    var imageBytes = imageXObject.GetImageBytes();
                    File.WriteAllBytes(imagePath, imageBytes);
                }

                ImageCount++;
            }



        }

        public ICollection<EventType> GetSupportedEvents()
        {
            return new EventType[] { EventType.RENDER_IMAGE };
        }

        #endregion

    }
}
