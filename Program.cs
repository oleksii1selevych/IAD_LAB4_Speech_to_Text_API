using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using WorkingWithGoogleCloudStorage;
using WorkingWithGoogleCloudStorage.CloudStorage;
using WorkingWithGoogleCloudStorage.Configuration;
using WorkingWithGoogleCloudStorage.SpeechToText;

ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
IConfiguration buildedConfiguration = configurationBuilder.AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
ApplicationConfiguration appConfig = buildedConfiguration.GetRequiredSection("ApplicationConfiguration").Get<ApplicationConfiguration>();


ICloudStorage cloudStorage = new GoogleCloudStorage(appConfig);

string inputAudioFileLocation = "C:\\Users\\Алексей\\Documents\\Аудиозаписи\\iad_lab4_recording.m4a";
string newWavMonoFileLocation = "D:\\Users\\User\\lab4Audio.wav";

FileConverter.ConvertToMonoWav(inputAudioFileLocation, newWavMonoFileLocation);

string myWavGcsUri = await cloudStorage.UploadFileAsync(newWavMonoFileLocation, "Lab4AudioTrack");


ISpeechToText speechToText = new SpeechToTextExecutor(appConfig);
await speechToText.RecognizeSpeech(myWavGcsUri);







