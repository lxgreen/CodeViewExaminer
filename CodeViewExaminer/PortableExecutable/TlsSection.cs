﻿using System;
using System.IO;

namespace Igloo
{
    public class TlsSectionReader : ISectionHandler
    {
        public bool CanHandle(string SectionName)
        {
            return SectionName == ".tls";
        }

        public CodeSection Handle(PeHeader PeHeader, PeSectionHeader Header, BinaryReader r)
        {
            TlsSection tls = new TlsSection();
            tls.SectionHeader = Header;
            tls.Is64 = !PeHeader.Is32BitHeader;

            if (tls.Is64)
            {
                tls.TlsDirectory64 = Misc.FromBinaryReader<IMAGE_TLS_DIRECTORY64>(r);
            }
            else
            {
                tls.TlsDirectory = Misc.FromBinaryReader<IMAGE_TLS_DIRECTORY32>(r);
            }
            return tls;
        }
    }

    public class TlsSection : CodeSection
    {
        public bool Is64;
        public IMAGE_TLS_DIRECTORY32 TlsDirectory;
        public IMAGE_TLS_DIRECTORY64 TlsDirectory64;
    }
}