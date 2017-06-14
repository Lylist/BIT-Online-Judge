namespace BITOJ.Data.Entities
{
    /// <summary>
    /// 对 SubmissionLanguage 枚举中的数据提供编码基准值。
    /// </summary>
    static class LanguageSystemEncodings
    {
        #region LanguageSuit
        public const uint C = 0x00010000;
        public const uint CPlusPlus = 0x00020000;
        public const uint Java = 0x00030000;
        public const uint Pascal = 0x00040000;
        public const uint Python = 0x00050000;
        #endregion

        #region CompilerSuit
        public const uint Gnu = 0x00000001;
        public const uint GnuCPP11 = 0x00000002;
        public const uint GnuCPP14 = 0x00000003;
        public const uint GnuCPP17 = 0x0000004;
        public const uint Microsoft = 0x00000005;
        public const uint Python2 = 0x00000001;
        public const uint Python3 = 0x00000002;
        #endregion
    }

    /// <summary>
    /// 表示一个提交所使用的程序设计语言和编译系统。
    /// </summary>
    public enum SubmissionLanguage : uint
    {
        /// <summary>
        /// GNU C 编译器。
        /// </summary>
        GnuC = LanguageSystemEncodings.C | LanguageSystemEncodings.Gnu,

        /// <summary>
        /// GNU C++ 编译器。
        /// </summary>
        GnuCPlusPlus = LanguageSystemEncodings.CPlusPlus | LanguageSystemEncodings.Gnu,

        /// <summary>
        /// GNU C++ 编译器，支持 C++11 标准。
        /// </summary>
        GnuCPlusPlus11 = LanguageSystemEncodings.CPlusPlus | LanguageSystemEncodings.GnuCPP11,

        /// <summary>
        /// GNU C++ 编译器，支持 C++14 标准。
        /// </summary>
        GnuCPlusPlus14 = LanguageSystemEncodings.CPlusPlus | LanguageSystemEncodings.GnuCPP14,

        /// <summary>
        /// GNU C++ 编译器，支持 C++17 标准。
        /// </summary>
        GnuCPlusPlus17 = LanguageSystemEncodings.CPlusPlus | LanguageSystemEncodings.GnuCPP17,

        /// <summary>
        /// 微软 C/C++ 优化编译器。
        /// </summary>
        MicrosoftCPlusPlus = LanguageSystemEncodings.CPlusPlus | LanguageSystemEncodings.Microsoft,

        /// <summary>
        /// Java 编译器。
        /// </summary>
        Java = LanguageSystemEncodings.Java,

        /// <summary>
        /// Pascal 编译器。
        /// </summary>
        Pascal = LanguageSystemEncodings.Pascal,

        /// <summary>
        /// Python 解释器，支持 Python 版本 2。
        /// </summary>
        Python2 = LanguageSystemEncodings.Python | LanguageSystemEncodings.Python2,

        /// <summary>
        /// Python 解释器，支持 Python 版本 3。
        /// </summary>
        Python3 = LanguageSystemEncodings.Python | LanguageSystemEncodings.Python3,
    }
}
