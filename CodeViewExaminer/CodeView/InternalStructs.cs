using System;
using System.Runtime.InteropServices;

namespace Igloo
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SubsectionDirectoryHeader
    {
        /// <summary>
        /// Length of directory header
        /// </summary>
        public ushort HeaderSize;

        /// <summary>
        /// Length of each directory entry.
        /// </summary>
        public ushort DirectoryEntrySize;

        /// <summary>
        /// Number of directory entries.
        /// </summary>
        public uint DirectoryCount;

        /// <summary>
        /// Offset from lfaBase of next directory. This field is currently unused,
        /// but is intended for use by the incremental linker to point to the next
        /// directory containing Symbol and Type OMF information from an
        /// incremental link.
        /// </summary>
        public uint lfoNextDir;

        /// <summary>
        /// Flags describing directory and subsection tables.
        /// No values have been defined for this field.
        /// </summary>
        public uint flags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DirectoryEntryHeader
    {
        /// <summary>
        /// Subdirectory index. See the table below for a listing of the valid subsection indices.
        /// </summary>
        public SubsectionType Type;

        /// <summary>
        /// Module index. This number is 1 based and zero (0) is never a valid
        /// index. The index = 0xffff is reserved for tables that are not associated
        /// with a specific module. These tables include sstLibraries,
        /// sstGlobalSym, sstGlobalPub, and sstGlobalTypes.
        /// </summary>
        public ushort iMod;

        /// <summary>
        /// Offset from the base address lfaBase.
        /// </summary>
        public uint ContentOffset;

        /// <summary>
        /// Number of bytes in subsection.
        /// </summary>
        public uint Size;
    }

    public enum SubsectionType : ushort
    {
        SSTModule = 0x120,
        SSTTypes = 0x121,
        SSTPublic = 0x122,
        SSTPublicSym = 0x123,
        SSTSymbols = 0x124,
        SSTAlignSym = 0x125,
        SSTSrcLnSeg = 0x126,
        SSTSrcModule = 0x127,
        SSTLibraries = 0x128,
        SSTGlobalSym = 0x129,
        SSTGlobalPub = 0x12a,
        SSTGlobalTypes = 0x12b,
        SSTMPC = 0x12c,
        SSTSegMap = 0x12d,
        SSTSegName = 0x12e,
        SSTPreComp = 0x12f,
        unused = 0x130,
        reserved = 0x131,
        reserved2 = 0x132,
        SSTFileIndex = 0x133,
        SSTStaticSym = 0x134,
    }
}