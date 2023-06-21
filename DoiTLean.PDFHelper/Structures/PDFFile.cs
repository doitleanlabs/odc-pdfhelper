using OutSystems.ExternalLibraries.SDK;

namespace DoiTLean.PDFHelper.Structures
{
    /// <summary>
    /// Image
    /// </summary>
    [OSStructure(Description = "PDFFile.")]
    public struct PDFFile
    {
        [OSStructureField(DataType = OSDataType.BinaryData, Description = "PDFBinaryData", IsMandatory = true)]
        public byte[] PDFBinaryData;

        public PDFFile(byte[] inPDFBinaryData) : this()
        {
            PDFBinaryData = inPDFBinaryData;
        }
    }

}