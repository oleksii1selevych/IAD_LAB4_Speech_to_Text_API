using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithGoogleCloudStorage.SpeechToText
{
    internal interface ISpeechToText
    {
        Task RecognizeSpeech(string resourseUri);
    }
}
