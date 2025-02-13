using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Trees
{
    public class VersionInfo
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public string CompanyName { get; set; }
        public string FileBuildPart { get; set; }
        public string FileDescription { get; set; }
        public string FileMajorPart { get; set; }
        public string FileMinorPart { get; set; }
        public string FileName { get; set; }
        public string FilePrivatePart { get; set; }
        public string FileVersion { get; set; }
        public string FileVersionRaw { get; set; }
        public string InternalName { get; set; }
        public string IsDebug { get; set; }
        public string IsPatched { get; set; }
        public string IsPreRelease { get; set; }
        public string IsPrivateBuild { get; set; }
        public string IsSpecialBuild { get; set; }
        public string Language { get; set; }
        public string LegalCopyright { get; set; }
        public string LegalTrademarks { get; set; }
        public string OriginalFilename { get; set; }
        public string PrivateBuild { get; set; }
        public string ProductBuildPart { get; set; }
        public string ProductMajorPart { get; set; }
        public string ProductMinorPart { get; set; }
        public string ProductName { get; set; }
        public string ProductPrivatePart { get; set; }
        public string ProductVersion { get; set; }
        public string ProductVersionRaw { get; set; }
        public string SpecialBuild { get; set; }
    }
}