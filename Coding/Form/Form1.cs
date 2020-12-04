
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Windows.Media.Imaging;


namespace Coding
{
    public partial class Form1 : Form
    {

        string ibinary;
        double chance = 0;
        Encode encode = new Encode();
        ChannelSend channel = new ChannelSend();
        ChannelSend channel2 = new ChannelSend();
        List<int> positions = new List<int>();
        List<int> positions2 = new List<int>();


        lasteleDecode lasteleD = new lasteleDecode();
        MDE lasteleMDE = new MDE();
        OutputDecode outputDecode = new OutputDecode();



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            
            ibinary = Input.Text;
            string result = encode.encode(ibinary);
            Output1.Text = result;
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Output_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            double.TryParse(ChanceBox1.Text, out double chance);
            string output = Output1.Text;
            int[] list = output.ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();

            positions =channel.sendChannel(list, chance);
            list = channel.changeBits(positions, list);

            var result = string.Join("", list.Select(x => x.ToString()).ToArray());

            ChannelOutput.Text = result;
            var result1 = string.Join(";", positions.Select(x => x.ToString()).ToArray());

            Positions1.Text = result1;
            positions.Clear();


        }

        private void ChannelOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string output = ChannelOutput.Text;
            int[] list = output.ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();

            decode(list);
           
            List<int> list2 = outputDecode.returnOutput();
            list2.RemoveRange(0, 6);
            
            var result = string.Join("", list2.Select(x => x.ToString()).ToArray());

           DecodedOutput1.Text = result;
            list2.Clear();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
        

        private void button7_Click(object sender, EventArgs e)
        {
            string text = TextInput.Text;


            string BinaryString = string.Join("", Encoding.UTF8.GetBytes(text).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));

            string result = encode.encode(BinaryString);
            OutputText.Text = result;

        }

       

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            double.TryParse(ChanceTextBox2.Text, out double chance);
            string output = OutputText.Text;
            string text = TextInput.Text;
            string BinaryString = string.Join("", Encoding.UTF8.GetBytes(text).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
            Console.WriteLine(BinaryString);
            int[] list = output.ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();
            int[] listNoEncode = BinaryString.ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();
            positions = channel.sendChannel(list, chance);
            positions2 = channel2.sendChannel(listNoEncode, chance);

            list = channel.changeBits(positions, list);
            listNoEncode= channel2.changeBits(positions2, listNoEncode);
            var result = string.Join("", list.Select(x => x.ToString()).ToArray());
            var resultNoEncode = string.Join("", listNoEncode.Select(x => x.ToString()).ToArray());

            TextChannelOutput.Text = result;
            TextNoEncode.Text = resultNoEncode;
            var result1 = string.Join(";", positions.Select(x => x.ToString()).ToArray());
            var result1Noencode = string.Join(";", positions2.Select(x => x.ToString()).ToArray());

       
            Positions2.Text = result1;
            Positions3.Text = result1Noencode;
            positions.Clear();
            positions2.Clear();

        }

        private void OutputText_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextChanelOutpiut_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string output = TextChannelOutput.Text;
            string text = TextNoEncode.Text;
            int[] list = output.ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();
            int[] listNoEncode = text.ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();

            decode(list);
            List<int> list2 = outputDecode.returnOutput();
            list2.RemoveRange(0, 6);

            var result = string.Join("", list2.Select(x => x.ToString()).ToArray());
            var result2 = string.Join("", listNoEncode.Select(x => x.ToString()).ToArray());

            byte[] bArr = convert2bytes(result);
            byte[] bArr2 = convert2bytes(result2);

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            DecodedText.Text = encoding.GetString(bArr);
            textBox4.Text = encoding.GetString(bArr2);
            list2.Clear();
        }

        private void SelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image files(*.jpg; *.jpeg; *.gif; *.bmp;*)|*.jpg; *.jpeg; *.gif; *.bmp;";
            if(open.ShowDialog()== DialogResult.OK)
            {
                textBoxP.Text = open.FileName;
                pictureBox1.Image = new Bitmap(open.FileName);
            }

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            double.TryParse(ImageChance.Text, out double chance);
            string image = textBoxP.Text;
            
            byte[] data1 = File.ReadAllBytes(image);

            


            byte[] head = new byte[54];

            for(int i = 0; i < 54; i++)
            {
                head[i]=data1[i];
            }
            var listB = data1.ToList();
            listB.RemoveRange(0, 54);

            byte[] data = listB.ToArray();

           

            //string result = encoding1.GetString(data);

            // var result1 = string.Join("", list.Select(x => x.ToString()).ToArray());
            BitArray bits = new BitArray(data);
            // string result = Convert.ToBase64String(data);
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }
            
            string BinaryString = sb.ToString();
            
               //string result = Encoding.UTF8.GetString(data, 0, data.Length);
              











              // string BinaryString = string.Join("", Encoding.UTF8.GetBytes(result).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));

                 string encoded= encode.encode(BinaryString);


                 int[] list = encoded.ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();


                 positions = channel.sendChannel(list, chance);

                 int [] resultList= channel.changeBits(positions, list);


                 decode(resultList);



                 List<int> list2 = outputDecode.returnOutput();
                 list2.RemoveRange(0, 6);

                 string result1 = string.Join("", list2.Select(x => x.ToString()).ToArray());
 
                 byte[] array = convert2bytes(result1);
                 byte[] arrayNoEncoding = convert2bytes(BinaryString);


            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                string string1 = encoding.GetString(array);
                string string1NoEncoding= encoding.GetString(arrayNoEncoding);

            // byte[] bytes= Encoding.UTF8.GetBytes(string1);
            //  byte[] bytes = Convert.FromBase64String(string1);
            byte[] bytes = Encoding.ASCII.GetBytes(string1);
            byte[] bytesNoEncoding = Encoding.ASCII.GetBytes(string1NoEncoding);
            Array.Copy(head,0, bytes,0, 54);
            Array.Copy(head, 0, bytesNoEncoding, 0, 54);
            var list4 = array.ToList();
               var list5 = head.ToList();
            list5 = list5.Concat(list4).ToList();

               bytes = list5.ToArray();

            var list10 = arrayNoEncoding.ToList();
            var list11 = head.ToList();
            list11 = list11.Concat(list10).ToList();
            
            bytesNoEncoding = list11.ToArray();


            list2.Clear();

               Bitmap bm;
               using (var ms= new MemoryStream(bytes))
               {
                   bm = new Bitmap(ms);
               }
            Bitmap bm2;
            using (var ms = new MemoryStream(bytesNoEncoding))
            {
                bm2 = new Bitmap(ms);
            }



            pictureBox2.Image = bm;
            pictureBox3.Image = bm2;




        }
        static String BitArrayToStr(BitArray ba)
        {
            byte[] strArr = new byte[ba.Length / 8];

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            for (int i = 0; i < ba.Length / 8; i++)
            {
                for (int index = i * 8, m = 1; index < i * 8 + 8; index++, m *= 2)
                {
                    strArr[i] += ba.Get(index) ? (byte)m : (byte)0;
                }
            }

            return encoding.GetString(strArr);
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void decode(int[] list)
        {
           
            for (int i = 0; i < list.Length; i += 2)
            {
                int result1 = lasteleD.return1(list[i], list[i + 1]); //
                int outputD = lasteleMDE.return1(result1);
                int decodedNumber = (outputD + lasteleD.return6()) % 2;
                lasteleD.insert(list[i]);
                outputDecode.insert(decodedNumber);
                lasteleMDE.insert(result1);
            }


        }


        private byte[] convert2bytes(String result)
        {
            byte[] bArr = new byte[result.Length / 8];
            for (int i = 0; i < result.Length / 8; i++)
            {
                String part = result.Substring(i * 8, 8);
                bArr[i] += Convert.ToByte(part, 2);
            }

            return bArr;
        }

        private void Image_Click(object sender, EventArgs e)
        {

        }

        private void DecodedText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
