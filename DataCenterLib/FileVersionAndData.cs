using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterLib
{
    /// <summary>
    /// Class to hold file contents and Version information
    /// This allows iC Data Center to process multiple versions of files, such as the S88 Process Definition File.
    /// </summary>
    [Serializable]
    public class FileVersionAndData
    {
        /// <summary>
        /// Version information
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Translation Language (if not null, then this is a language translation)
        /// (If null, then this is the default S88 definitions.)
        /// </summary>
        public string TranslationLanguage { get; set; }

        /// <summary>
        /// Entire file Contents
        /// </summary>
        public byte[] Contents { get; set; }

        /// <summary>
        /// File Contents as an XDocument (useful for parsing XML) as a string
        /// </summary>
        public string XDocument { get; set; }
    }
}
