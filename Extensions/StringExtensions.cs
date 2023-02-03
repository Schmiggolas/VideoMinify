using System;
using System.IO;
using System.Linq;

namespace VideoMinify
{
    public static class StringExtensions
    {
        public static bool IsValidVideoPath(this string path)
        {
            return File.Exists(path) && path.EndsWithVideoFileExtension();
        }

        private static bool EndsWithVideoFileExtension(this string path)
        {
            var fileExtensions = new[]
            {
                ".WEBM", ".MKV", ".FLV", ".VOB", ".OGV", ".OGG", ".GIFV",
                ".MNG", ".AVI", ".MTS", ".M2TS", ".TS", ".MOV", ".QT",
                ".WMV", ".YUV", ".M4V", ".MPG", ".MPEG", ".M2V", ".AMV",
                ".MP4"
            };
            return fileExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase);
        }
    }
}