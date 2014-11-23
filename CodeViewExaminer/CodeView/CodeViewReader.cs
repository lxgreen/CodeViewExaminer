using System;
using System.IO;
using System.Text;

// https://github.com/aBothe/CodeViewExaminer

namespace Igloo
{
    public class CodeViewReader
    {
        public const string CodeViewSignature = "NB09";

        private IMAGE_DEBUG_DIRECTORY _imageDebugDirectory;
        private BinaryReader _binaryReader;
        private CodeViewData _codeViewData = new CodeViewData();
        protected static readonly ILog _Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Reads debug information from a file handle.
        /// The hFile parameter has to be closed afterwards manually(!).
        /// </summary>
        public static CodeViewData Read(IntPtr hFile, long debugInfoOffset, long debugInfoSize)
        {
            if (debugInfoSize == 0)
                return null;

            using (FileStream file = new FileStream(hFile, FileAccess.Read))
            {
                using (BinaryReader r = new BinaryReader(file))
                {
                    file.Position = debugInfoOffset;
                    CodeViewReader cvReader = new CodeViewReader { _binaryReader = r };
                    cvReader.DoRead();

                    return cvReader._codeViewData;
                }
            }
        }

        public static CodeViewData Read(IMAGE_DEBUG_DIRECTORY ddir, BinaryReader r)
        {
            CodeViewReader cvReader = new CodeViewReader { _imageDebugDirectory = ddir, _binaryReader = r };

            cvReader.DoRead();

            return cvReader._codeViewData;
        }

        private void DoRead()
        {
            /*
             * For more info, see codeviewNB09.pdf, point 7. "Symbol and Type Format for Microsoft Executables"
             * (pdf page 71)
             */

            _codeViewData.lfaBase = _binaryReader.BaseStream.Position;

            // Ensure that there's the right CodeView4 signature
            string signature = Encoding.ASCII.GetString(_binaryReader.ReadBytes(4));
            if (signature != CodeViewSignature)
            {
                _Log.Error("Invalid Data Exception", new InvalidDataException("Invalid CodeView Format: Signature '" + CodeViewSignature + "' expected, '" + signature + "' found at position " + _codeViewData.lfaBase));
            }
            // Read 'Subsection Directory' address
            _codeViewData.lfoDirectory = _binaryReader.ReadUInt32();

            _binaryReader.BaseStream.Position = _codeViewData.lfaBase + _codeViewData.lfoDirectory;

            _codeViewData.SubsectionDirectory = SubsectionDirectory.Read(_codeViewData.lfaBase, _binaryReader);
        }
    }
}