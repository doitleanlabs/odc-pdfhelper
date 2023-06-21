using OutSystems.ExternalLibraries.SDK;

namespace DoiTLean.PDFHelper.Structures
{
    /// <summary>
    /// Image
    /// </summary>
    [OSStructure(Description = "Image.")]
    public struct Image
    {
        [OSStructureField(DataType = OSDataType.BinaryData, Description = "Binary", IsMandatory = true)]
        public byte[] binary;

        [OSStructureField(DataType = OSDataType.Text, Description = "File extension", IsMandatory = true)]
        public string fileExtension;

        public Image(byte[] inBinary,string inFileExtension) : this()
        {
            binary = inBinary;
            fileExtension = inFileExtension ?? string.Empty;
        }
    }

}