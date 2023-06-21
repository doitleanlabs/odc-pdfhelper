using iText.Kernel.Pdf;
using OutSystems.ExternalLibraries.SDK;
using System.Globalization;
using System;

namespace DoiTLean.PDFHelper.Structures
{
    /// <summary>
    /// Image
    /// </summary>
    [OSStructure(Description = "PDFComment.")]
    public struct PDFComment
    {
       
        [OSStructureField(DataType = OSDataType.Text, Description = "Internal ID", IsMandatory = true)]
        public string Id;

        [OSStructureField(DataType = OSDataType.Text, Description = "Page Number where the comment is.", IsMandatory = true)]
        public string Page;

        [OSStructureField(DataType = OSDataType.Text, Description = "Author Name", IsMandatory = true)]
        public string Author;

        [OSStructureField(DataType = OSDataType.Text, Description = "Comment", IsMandatory = true)]
        public string Comment;

        [OSStructureField(DataType = OSDataType.DateTime, Description = "Datetime", IsMandatory = true)]
        public DateTime datetime;



        public PDFComment(string inId, string inPage, string inAuthor, string inComment, DateTime inDatetime) : this()
        {
            Id = inId ?? string.Empty;
            Page = inPage ?? string.Empty;
            Author = inAuthor ?? string.Empty;
            Comment = inComment ?? string.Empty;
//            datetime = inDatetime ?? DateTime.UtcNow;

        }
    }

}