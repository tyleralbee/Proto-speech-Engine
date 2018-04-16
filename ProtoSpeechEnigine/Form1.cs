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

namespace ProtoSpeechEnigine
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechRecognitionEngine recEngine2 = new SpeechRecognitionEngine();
        SpeechRecognitionEngine recEngine3 = new SpeechRecognitionEngine();


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Choices states = new Choices();
            states.Add(new string[] { "review mode", "edit mode" });

            GrammarBuilder sBuilder = new GrammarBuilder();
            sBuilder.Append(states);
            Grammar stateGrammar = new Grammar(sBuilder);
            recEngine.LoadGrammarAsync(stateGrammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
            
        }

        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "edit mode"){
                Choices commands = new Choices();
                commands.Add(new string[] { "hello", "hello world", "next", "previous", "prev" });
                GrammarBuilder gBuilder = new GrammarBuilder();
                gBuilder.Append(commands);
                Grammar grammar = new Grammar(gBuilder);
                recEngine2.LoadGrammarAsync(grammar);
                recEngine2.SpeechRecognized += RecEngine_SpeechRecognized;
                switch (e.Result.Text)
                {
                    case "hello":
                        richTextBox1.Text += "\nhello";
                        break;
                    case "hello world":
                        richTextBox1.Text += "\nhello world";
                        break;
                    case "next":
                        richTextBox1.Text += "\nnext";
                        break;
                    case "previous":
                        richTextBox1.Text += "\nprevious";
                        break;
                    case "prev":
                        richTextBox1.Text += "\nprev";
                        break;
                    default:
                        richTextBox1.Text += "\nedit mode";
                        break;
                }
            }
            else if (e.Result.Text == "review mode")
            {
                Choices commands2 = new Choices();

                commands2.Add(new string[] { "no", "yes" });

                GrammarBuilder gBuilder2 = new GrammarBuilder();

                gBuilder2.Append(commands2);

                Grammar grammar2 = new Grammar(gBuilder2);

                recEngine3.LoadGrammarAsync(grammar2);

                recEngine3.SpeechRecognized += RecEngine_SpeechRecognized;

                switch (e.Result.Text)
                {
                    case "no":
                        richTextBox1.Text += "\nhello";
                        break;
                    case "yes":
                        richTextBox1.Text += "\nhello world";
                        break;

                    default:
                        richTextBox1.Text += "\nreview mode";
                        break;
                }
            }



        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            btnDisable.Enabled = false;
        }
    }
}
