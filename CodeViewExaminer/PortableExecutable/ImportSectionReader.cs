using System;

namespace Igloo
{
    public class ImportSectionReader : ISectionHandler
    {
        public bool CanHandle(string SectionName)
        {
            return SectionName == ".idata";
        }

        public CodeSection Handle(PeHeader PeHeader, PeSectionHeader Header, System.IO.BinaryReader Reader)
        {
            ImportSection imps = new ImportSection();

            return imps;
        }
    }

    public class ImportSection : CodeSection
    {
    }
}