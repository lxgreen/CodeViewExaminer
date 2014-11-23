using System;

// https://github.com/aBothe/CodeViewExaminer
namespace Igloo
{
    public class CodeViewData
    {
        /// <summary>
        /// The absolute base address for CV-related things
        /// </summary>
        public long lfaBase;

        /// <summary>
        /// Offset of the subsection directory.
        /// </summary>
        public uint lfoDirectory;

        public SubsectionDirectory SubsectionDirectory;
    }
}