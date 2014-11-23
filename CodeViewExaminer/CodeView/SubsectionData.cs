using System;
using System.IO;

namespace Igloo
{
    public abstract class SubsectionData
    {
        protected static readonly ILog _Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static SubsectionData Read(long lfaBase, DirectoryEntryHeader hdr, BinaryReader r)
        {
            SubsectionData sd = null;

            switch (hdr.Type)
            {
                case SubsectionType.SSTModule:
                    sd = new SSTModule();
                    break;

                case SubsectionType.SSTSrcModule:
                    sd = new SSTSrcModule();
                    break;

                case SubsectionType.SSTLibraries:
                    sd = new SSTLibraries();
                    break;

                case SubsectionType.SSTGlobalSym:
                    sd = new SSTGlobalSym();
                    break;

                case SubsectionType.SSTGlobalTypes:
                    sd = new SSTGlobalTypes();
                    break;

                case SubsectionType.SSTSegName:
                    sd = new SSTSegName();
                    break;

                case SubsectionType.SSTFileIndex:
                    sd = new SSTFileIndex();
                    break;
            }

            if (sd != null)
            {
                sd.Header = hdr;
                r.BaseStream.Position = lfaBase + hdr.ContentOffset;
                sd.Read(r);
            }

            return sd;
        }

        public DirectoryEntryHeader Header;

        public abstract void Read(BinaryReader reader);
    }
}