using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Media;


namespace SportoAppVR
{    
    
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        //SoundPlayer player = new SoundPlayer("C:/Users/Marius/Downloads/beep.wav");

        public Form1()
        {
            InitializeComponent();


        }

        private void Play()
        {
            string soundfile = @"C:/Users/Marius/Downloads/warningbeep.wav";
            var sound = new SoundPlayer(soundfile);
            sound.Play();
        }

        int sekundes = 1;
        int kartai = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblSekundes.Text =""+ sekundes++;

            if(sekundes == 11)
            {
                lblSekundes.Text = "0";
                lblKartai.Text = ""+ ++kartai;
                Play();
                sekundes = 1;
                
            }
            if (kartai >= 20)
            {
                timer1.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "start", "stop", "reset"});
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch(e.Result.Text)
            {
                case "start":
                    Play();
                    //MessageBox.Show("Hello, Marius. How are you?");

                    timer1.Enabled = true;

                    break;
                case "stop":
                    timer1.Enabled = false;
                    break;
                case "reset":
                    lblSekundes.Text = "0";
                    lblKartai.Text = "0";
                    kartai = 0;
                    sekundes = 1;
                    break;
                
                  
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
           // Play();
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            //recEngine.RecognizeAsyncStop();
            //btnDisable.Enabled = false;
        }
    }
}
