using System;
using System.IO;

namespace Igloo
{
    public class SubsectionDirectory
    {
        public SubsectionDirectoryHeader Header;
        public SubsectionData[] Sections;

        public static SubsectionDirectory Read(long lfaBase, BinaryReader r)
        {
            // Read directory section header
            SubsectionDirectoryHeader hdr = Misc.FromBinaryReader<SubsectionDirectoryHeader>(r);

            SubsectionDirectory sd = new SubsectionDirectory();
            sd.Header = hdr;

            // Read entry headers
            DirectoryEntryHeader[] eheaders = new DirectoryEntryHeader[(int)hdr.DirectoryCount];
            for (int i = 0; i < hdr.DirectoryCount; i++)
            {
                eheaders[i] = Misc.FromBinaryReader<DirectoryEntryHeader>(r);
            }

            // Read directory entry contents
            sd.Sections = new SubsectionData[hdr.DirectoryCount];
            for (int i = 0; i < hdr.DirectoryCount; i++)
            {
                sd.Sections[i] = SubsectionData.Read(lfaBase, eheaders[i], r);
            }

            return sd;
        }
    }
}