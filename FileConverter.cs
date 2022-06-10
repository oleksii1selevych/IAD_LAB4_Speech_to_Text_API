using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace WorkingWithGoogleCloudStorage
{
    internal static class FileConverter
    {
        public static void ConvertToMonoWav(string audioFilePath, string monoAudioPath)
        {
            var stream = new MemoryStream();
            using (var data = new MediaFoundationReader(audioFilePath))
            {
                var waveFormat = new WaveFormatConversionStream(new WaveFormat(16000, 1), data);
                WaveFileWriter.WriteWavFileToStream(stream, waveFormat);

                byte[] bytesArray = stream.ToArray();
                File.WriteAllBytes(monoAudioPath, bytesArray);
            }
        }
    }
}


