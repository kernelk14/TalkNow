using System;
using System.Diagnostics;
using System.Globalization;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using Mscc.GenerativeAI;
using dotenv.net;

namespace YouShouldTalk     
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var root = Directory.GetCurrentDirectory();
            var answer = "";
            var env = DotEnv.Read();
            Console.WriteLine(root);
            var apiKey = env["GOOGLE_API_KEY"];
            //var apiKey = "AIzaSyAjF54cRKauxE-0KQJXDY3puuNtgO89z3I";
            bool running = true;
            while (running)
            {
                Console.Write("> ");
                var prompt = Console.ReadLine();
                var googleAi = new GoogleAI(apiKey: apiKey);
                var model = googleAi.GenerativeModel(model: Model.Gemini20Flash);

                var synth = new SpeechSynthesizer();
                synth.SetOutputToDefaultAudioDevice();

                var response = await model.GenerateContent(prompt);
                answer += response;
                Console.WriteLine(response);
                synth.Speak(answer);
                answer = "";
            }
        }
    }
}