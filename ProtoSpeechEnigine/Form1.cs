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
            initializeStates();
            initializeEditCommands();
            initializeReviewCommands();
        }

        private void RecEngine_StateRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "edit mode":
                    richTextBox1.Text += "\nedit mode";
                    recEngine.RecognizeAsyncStop();
                    initializeRec2();
                    break;
                case "review mode":
                    richTextBox1.Text += "\nreview mode";
                    recEngine.RecognizeAsyncStop();
                    initializeRec3();
                    break;
                default:
                    richTextBox1.Text += "\ndefault mode";
                    break;
            }
        }
        private void RecEngine_EditCommandRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "next":
                    richTextBox1.Text += "\nnext";
                    break;
                case "previous":
                    richTextBox1.Text += "\nprevious";
                    break;
                case "prev":
                    richTextBox1.Text += "\nprev";
                    break;
                case "choose mode":
                    richTextBox1.Text += "\nchoose mode";
                    recEngine2.RecognizeAsyncStop();
                    initializeRec1();
                    break;
                default:
                    richTextBox1.Text += "\ndefault";
                    break;
            }
        }
        private void RecEngine_ReviewCommandRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "next":
                    richTextBox1.Text += "\nnext";
                    break;
                case "previous":
                    richTextBox1.Text += "\nprevious";
                    break;
                case "prev":
                    richTextBox1.Text += "\nprev";
                    break;
                case "root":
                    richTextBox1.Text += "\nroot";
                    break;
                case "choose mode":
                    richTextBox1.Text += "\nchoose mode";
                    recEngine3.RecognizeAsyncStop();
                    initializeRec1();
                    break;
                default:
                    richTextBox1.Text += "\ndefault";
                    break;
            }
        }

        private void initializeRec1()
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            recEngine.SpeechRecognized += RecEngine_StateRecognized;
        }
        private void initializeRec2()
        {
            recEngine2.RecognizeAsync(RecognizeMode.Multiple);
            recEngine2.SpeechRecognized += RecEngine_EditCommandRecognized;
        }
        private void initializeRec3()
        {
            recEngine3.RecognizeAsync(RecognizeMode.Multiple);
            recEngine3.SpeechRecognized += RecEngine_ReviewCommandRecognized;
        }

        private void initializeStates()
        {
            Choices states = new Choices();
            states.Add(new string[] { "review mode", "edit mode" });
            GrammarBuilder sBuilder = new GrammarBuilder();
            sBuilder.Append(states);
            Grammar stateGrammar = new Grammar(sBuilder);
            recEngine.LoadGrammarAsync(stateGrammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += RecEngine_StateRecognized;
        }
        private void initializeEditCommands()
        {
            Choices eCommands = new Choices();
            eCommands.Add(new string[] { "next", "previous", "prev", "choose mode" });
            GrammarBuilder eBuilder = new GrammarBuilder();
            eBuilder.Append(eCommands);
            Grammar editGrammar = new Grammar(eBuilder);
            recEngine2.LoadGrammarAsync(editGrammar);
            recEngine2.SetInputToDefaultAudioDevice();
        }
        private void initializeReviewCommands()
        {
            Choices rCommands = new Choices();
            rCommands.Add(new string[] { "next", "previous", "prev", "root", "choose mode" });
            GrammarBuilder rBuilder = new GrammarBuilder();
            rBuilder.Append(rCommands);
            Grammar reviewGrammar = new Grammar(rBuilder);
            recEngine3.LoadGrammarAsync(reviewGrammar);
            recEngine3.SetInputToDefaultAudioDevice();
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            btnDisable.Enabled = false;
        }
    }
}
