using OutSystems.ExternalLibraries.SDK;

namespace DoiTLean.PDFHelper.Structures
{
    /// <summary>
    /// ImageRecord
    /// </summary>
    [OSStructure(Description = "ImageRecord.")]
    public struct ImageRecord
    {
        [OSStructureField(DataType = OSDataType.BinaryData, Description = "Binary", IsMandatory = true)]
        public byte[] binary;

        [OSStructureField(DataType = OSDataType.Text, Description = "File extension", IsMandatory = true)]
        public string fileExtension;

        public ImageRecord(byte[] inBinary,string inFileExtension) : this()
        {
            binary = inBinary;
            fileExtension = inFileExtension ?? string.Empty;
        }
    }

}