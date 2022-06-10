using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithGoogleCloudStorage.Configuration
{
    internal class ApplicationConfiguration
    {
        public string GoogleCloudCredentialFile { get; set; } = null!;
        public string GoogleCloudStorageBucket { get; set; } = null!;
    }
}
