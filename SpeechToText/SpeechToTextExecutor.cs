using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using Grpc.Auth;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWithGoogleCloudStorage.Configuration;

namespace WorkingWithGoogleCloudStorage.SpeechToText
{
    internal class SpeechToTextExecutor : ISpeechToText
    {
        private readonly SpeechClientBuilder speechClientBuilder;
        private const string AudioTranscriptionPath = @"D:\Users\User\RecognizedFile.txt";

        public SpeechToTextExecutor(ApplicationConfiguration configuration)
        {
            GoogleCredential googleCredential = GoogleCredential.FromFile(configuration.GoogleCloudCredentialFile);
            speechClientBuilder = new SpeechClientBuilder();
            speechClientBuilder.CredentialsPath = configuration.GoogleCloudCredentialFile;
        }   


        public async Task RecognizeSpeech(string resourseUri)
        {
            var speechRecognizer = await speechClientBuilder.BuildAsync();

            var config = new RecognitionConfig
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 16000,
                LanguageCode = LanguageCodes.English.UnitedStates
            };

            var audio = RecognitionAudio.FromStorageUri(resourseUri);

            var response = await speechRecognizer.RecognizeAsync(config, audio);

            var recognizedText = "";

            foreach( var result in response.Results)
            {
                recognizedText += String.Format("{0} ", result.Alternatives[0].Transcript);
                Console.WriteLine(result.Alternatives[0]);
            }

            await File.WriteAllTextAsync(AudioTranscriptionPath, recognizedText);
        }
    }
}
