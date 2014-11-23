using System.IO;

// https://github.com/aBothe/CodeViewExaminer

namespace Igloo
{
    public class DebugSectionReader : ISectionHandler
    {
        public bool CanHandle(string SectionName)
        {
            return SectionName == ".debug";
        }

        public CodeSection Handle(PeHeader PeHeader, PeSectionHeader hdr, BinaryReader reader)
        {
            IMAGE_DEBUG_DIRECTORY entryInfo = Misc.FromBinaryReader<IMAGE_DEBUG_DIRECTORY>(reader);

            if (entryInfo.PointerToRawData == 0)
            {
                return null;
            }

            reader.BaseStream.Position = entryInfo.PointerToRawData;

            if (entryInfo.Type == IMAGE_DEBUG_TYPE.CODEVIEW)
            {
                CodeViewDebugSection codeViewDebugSection = new CodeViewDebugSection();

                codeViewDebugSection.EntryInformation = entryInfo;
                codeViewDebugSection.SectionHeader = hdr;
                codeViewDebugSection.Data = CodeViewReader.Read(entryInfo, reader);
                return codeViewDebugSection;
            }

            DebugSection dSection = new DebugSection();
            dSection.SectionHeader = hdr;
            dSection.EntryInformation = entryInfo;
            return dSection;
        }
    }

    public class DebugSection : CodeSection
    {
        public IMAGE_DEBUG_DIRECTORY EntryInformation;
    }

    /// <summary>
    /// A dedicated section object which stores CodeView information
    /// </summary>
    public class CodeViewDebugSection : DebugSection
    {
        public CodeViewData Data;
    }
}