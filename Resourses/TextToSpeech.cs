using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.Globalization;

namespace SACSA.Resourses
{
    public partial class TextToSpeech
    {
        CultureInfo Idioma = new CultureInfo("es-CO");
        public void TextSpeech(object texto) 
        {
            SpeechSynthesizer voz = new SpeechSynthesizer();
            voz.SetOutputToDefaultAudioDevice();
            string usevoice = "Microsoft Sabina Desktop";
            voz.SelectVoice(usevoice);
            voz.Speak(texto.ToString());
            var builder = new PromptBuilder();
            builder.StartVoice(Idioma);
            builder.EndVoice();
        }
       
        public void SpeechToText() 
        {
            SpeechRecognitionEngine GrabadoraDeVoz = new SpeechRecognitionEngine(Idioma); 
            GrabadoraDeVoz.SetInputToDefaultAudioDevice();
            GrabadoraDeVoz.LoadGrammar(new DictationGrammar());
            GrabadoraDeVoz.RecognizeAsync(RecognizeMode.Multiple);
            GrabadoraDeVoz.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(CapturarVoz);

                Console.ReadKey();
           
        }

        private void CapturarVoz(object sender, SpeechRecognizedEventArgs e)
        {
           Console.WriteLine(e.Result.Text);
        }
    }
}
